using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<WebApplication1.Models.Users> Users { get; set; }
        public DbSet<WebApplication1.Models.Course> Courses { get; set; }
        public DbSet<WebApplication1.Models.Module> Modules { get; set; }
        public DbSet<WebApplication1.Models.Topic> Topics { get; set; }
      // public DbSet<WebApplication1.Models.UserProgress> Userprogress { get; set; }
    }
}
