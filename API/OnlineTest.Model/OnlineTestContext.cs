using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineTest.Model;

namespace OnlineTest
{
    public class OnlineTestContext : DbContext
    {
        public OnlineTestContext(DbContextOptions<OnlineTestContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}