using DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Thumbnail> Thumbnails { get; set; }
    }
}
