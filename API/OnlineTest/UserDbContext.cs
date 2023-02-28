using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineTest.Model;

namespace OnlineTest
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        { }

    }
}