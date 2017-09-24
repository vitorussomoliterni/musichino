using Microsoft.EntityFrameworkCore;

namespace musichino.Data.Models
{
    public class MusichinoDbContext : DbContext
    {
        public DbSet<ArtistModel> Astist { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<ActionModel> Action { get; set; }
    }
}