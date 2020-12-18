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
    [Route("api/[controller]")]
    [ApiController]
    public class SourcesController : ControllerBase
    {
        private readonly BlogContext _context;

        public SourcesController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var sources = await _context.Sources
                .Select(t => new SourceDto
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(sources);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var source = await _context.Sources
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return Ok(source);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var source = await _context.Sources.FirstOrDefaultAsync(t => t.Id == id);

            _context.Sources.Remove(source);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SourceDto sourceDto)
        {
            var sourceEntity = await _context.Sources.FirstOrDefaultAsync(t => t.Id == id);
            sourceEntity.Name = sourceDto.Name;

            _context.Sources.Update(sourceEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SourceDto source)
        {
            await _context.Sources.AddAsync(new Source { Name = source.Name });
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}