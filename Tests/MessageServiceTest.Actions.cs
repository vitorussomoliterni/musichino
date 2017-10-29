using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using musichino.Data.Models;
using musichino.Services;
using Xunit;
using static musichino.Services.MessageService;

namespace Tests
{
    public partial class MessageServiceTest
    {
        [Fact]
        public async Task PerformActionTest_ShouldSuspendUserActivity()
        {
            //Given
            var service = new MessageService();
            var options = TestHelper.optionsFactory("suspend_user_db");
            var expectedUser = new UserModel()
            {
                Id = new Guid(),
                ExternalId = 1,
                CreatedAtUtc = DateTime.UtcNow,
                IsActive = false
            };

            using (var context = new MusichinoDbContext(options))
            {
                await context.User.AddAsync(expectedUser);
                await context.SaveChangesAsync();
            }

            //When
            using (var context = new MusichinoDbContext(options))
            {
                await service.PerformAction(Commands.Suspend, expectedUser.Id, context);
                var actualUser = await context.User.FirstOrDefaultAsync(u => u.Id == expectedUser.Id);

                //Then
                Assert.NotNull(actualUser);
                Assert.Equal(actualUser.IsActive, false);
            }
        }
    }
}