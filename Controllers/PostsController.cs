using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyBlog.Models;
using EasyBlog.Persistence;
using EasyBlog.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _context.Posts
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedTime = p.CreatedTime,
                    AuthorName = p.Author.FullName,
                    AuthorAvatar = p.Author.Avatar,
                    Tags = p.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList(),
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _context.Posts
                .Where(p => p.Id == id)
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedTime = p.CreatedTime,
                    AuthorName = p.Author.FullName,
                    AuthorAvatar = p.Author.Avatar,
                    Tags = p.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList(),
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PostCreateEditDto post)
        {
            var postEntity = await _context.Posts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);
            var tags = _context.Tags
                .Where(t => post.TagIds.Contains(t.Id))
                .ToList();

            postEntity.Content = post.Content;
            postEntity.Title = post.Title;
            postEntity.Tags = new List<Tag>();
            _context.Posts.Update(postEntity);
            await _context.SaveChangesAsync();


            postEntity.Tags = tags;
            _context.Posts.Update(postEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostCreateEditDto post)
        {
            var tags = _context.Tags
                .Where(t => post.TagIds.Contains(t.Id))
                .ToList();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.Claims.First(c => c.Type == "UserId").Value;

            await _context.Posts.AddAsync(new Post
            {
                CreatedTime = DateTime.Now,
                Content = post.Content,
                Title = post.Title,
                Tags = tags,
                AuthorId = Guid.Parse(userId ?? string.Empty),
            });
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}