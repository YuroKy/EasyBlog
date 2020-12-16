using System;
using System.Linq;
using System.Threading.Tasks;
using EasyBlog.Models;
using EasyBlog.Persistence;
using EasyBlog.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly BlogContext _context;

        public TagsController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var tags = await _context.Tags
                .Select(t => new TagDto
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(tags);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tag = await _context.Tags
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return Ok(tag);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TagDto tag)
        {
            var tagEntity = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
            tagEntity.Name = tag.Name;

            _context.Tags.Update(tagEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TagDto tag)
        {
            await _context.Tags.AddAsync(new Tag { Name = tag.Name });
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}