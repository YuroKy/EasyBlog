﻿using System;
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
    [Route("api/[controller]")]
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
                    Tags = p.PostTags.Select(t => new TagDto { Id = t.TagId, Name = t.Tag.Name }).ToList(),
                    Source = p.Source != null ? new SourceDto { Id = p.Source.Id, Name = p.Source.Name } : new SourceDto(),
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(posts);
        }

        [AllowAnonymous]
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
                    Tags = p.PostTags.Select(t => new TagDto { Id = t.TagId, Name = t.Tag.Name }).ToList(),
                    Source = p.Source != null ? new SourceDto { Id = p.Source.Id, Name = p.Source.Name } : new SourceDto(),
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
                .Include(p => p.PostTags)
                .FirstOrDefaultAsync(p => p.Id == id);
            var tags = _context.Tags
                .Where(t => post.TagIds.Contains(t.Id))
                .ToList();

            postEntity.Content = post.Content;
            postEntity.Title = post.Title;
            postEntity.SourceId = post.SourceId;
            postEntity.PostTags = new List<PostTag>();

            _context.Posts.Update(postEntity);
            await _context.SaveChangesAsync();


            postEntity.PostTags = tags.Select(t => new PostTag{PostId = postEntity.Id, TagId = t.Id}).ToList();
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
                PostTags = tags.Select(t => new PostTag { TagId = t.Id }).ToList(),
                AuthorId = Guid.Parse(userId ?? string.Empty),
                SourceId = post.SourceId,
            });
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}