using Microsoft.EntityFrameworkCore;
using musichino.Data.Models;

namespace Tests
{
    internal class TestHelper
    {
        internal static DbContextOptions<MusichinoDbContext> optionsFactory(string dbName)
        {
            var options = new DbContextOptionsBuilder<MusichinoDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return options;
        }
    }
}