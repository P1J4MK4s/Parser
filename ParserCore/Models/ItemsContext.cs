using Microsoft.EntityFrameworkCore;

namespace ParserCore.Models
{
    public class ItemsContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Tree> Trees { get; set; }
        public DbSet<FirstSub> FirstSubs { get; set; }
        public DbSet<SecondSub> SecondSubs { get; set; }
        public DbSet<Part> Parts { get; set; }
        public ItemsContext(DbContextOptions<ItemsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
