using Microsoft.EntityFrameworkCore;

namespace musichino.Data.Models
{
    public class MusichinoDbContext : DbContext
    {
        public MusichinoDbContext()
        { }
        public MusichinoDbContext(DbContextOptions<MusichinoDbContext> options)
            : base(options)
        { }
        public DbSet<ArtistModel> Artists { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ActionModel> Actions { get; set; }
        public DbSet<ArtistUserModel> ArtistUsers { get; set; }
    }
}