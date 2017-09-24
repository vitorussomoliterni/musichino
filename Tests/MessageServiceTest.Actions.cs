using Microsoft.EntityFrameworkCore;
using musichino.Data.Models;

namespace Tests
{
    public partial class MessageServiceTest
    {
        private DbContextOptions coptionsFactory(string dbName)
        {
            var options = new DbContextOptionsBuilder<MusichinoDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return options;
        }
    }
}