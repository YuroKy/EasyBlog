using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EasyBlog.Models;
using EasyBlog.Persistence;
using EasyBlog.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EasyBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BlogContext _context;

        public AuthController(BlogContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInDto credentials)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == credentials.Username);

            if (user == null || user.Status == UserStatus.Blocked)
            {
                return BadRequest("Invalid username or password");
            }

            using (var hmac = new HMACSHA512(user.Salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(credentials.Password));
                if (!user.Password.SequenceEqual(computedHash))
                {
                    return BadRequest("Invalid username or password");
                }
            }

            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim("UserId", user.Id.ToString()),
                },
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(encodedJwt);
        }
    }
}