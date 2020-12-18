using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EasyBlog.Persistence;
using EasyBlog.Persistence.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EasyBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BlogContext>();
                context.Database.Migrate();
                if (!context.Users.Any())
                {
                    var (passwordHash, passwordSalt) = GetHashedPassword("pass");

                    context.Users.Add(new User
                    {
                        Email = "yurii.fedelesh@gmail.com",
                        FirstName = "Yurii",
                        LastName = "Fedelesh",
                        Password = passwordHash,
                        Salt = passwordSalt,
                        Status = UserStatus.Active,
                        RegistrationTime = DateTime.Now,
                        Username = "yurii",
                    });

                    context.SaveChanges();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static (byte[], byte[]) GetHashedPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var (passwordHash, passwordSalt) = (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
                return (passwordHash, passwordSalt);
            }
        }
    }
}
