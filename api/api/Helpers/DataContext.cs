using DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Hls264> Hls264 { get; set; }
        public DbSet<MultibitHls> MultibitHls { get; set; }
    }
}
