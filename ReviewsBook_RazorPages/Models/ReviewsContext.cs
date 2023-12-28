using Microsoft.EntityFrameworkCore;

namespace ReviewsBook_RazorPages.Models
{
    public class ReviewsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public ReviewsContext(DbContextOptions<ReviewsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
