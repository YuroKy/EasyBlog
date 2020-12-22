using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using EasyBlog.Models;
using EasyBlog.Persistence;
using EasyBlog.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace EasyBlog.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly BlogContext _context;

        public ExportController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet("{entityName}")]
        public FileContentResult Export(string entityName)
        {
            var records = ResolveData(entityName);
            byte[] csvData;

            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(records);
                csvData = Encoding.UTF8.GetBytes(writer.ToString());
            }

            return new FileContentResult(csvData, "application/octet-stream")
            {
                FileDownloadName = $"{entityName}_{DateTime.Now}.csv"
            };
        }


        private dynamic ResolveData(string entityName)
        {
            switch (entityName)
            {
                case nameof(Post):
                    return _context.Posts
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
                        .ToList();
                case nameof(Source):
                    return _context.Sources
                        .Select(s => new SourceDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                        })
                        .ToList();
                case nameof(Tag):
                    return _context.Tags
                        .Select(t => new TagDto
                        {
                            Id = t.Id,
                            Name = t.Name,
                        })
                        .ToList();
                case nameof(User):
                    return _context.Users
                        .Select(u => new UserDto
                        {
                            Username = u.Username,
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            RegistrationTime = u.RegistrationTime,
                            Email = u.Email,
                            Status = u.Status,
                            Avatar = u.Avatar,
                        })
                        .ToList();
                default:
                    return new List<BaseEntity>();
            }
        }
    }
}