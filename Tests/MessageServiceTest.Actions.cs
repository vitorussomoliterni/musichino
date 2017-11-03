using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using musichino.Commands;
using musichino.Data.Models;
using musichino.Services;
using Xunit;
using static musichino.Services.MessageService;

namespace Tests
{
    public partial class MessageServiceTest
    {
        [Fact]
        public async Task PerformActionTest_ShouldAddArtist()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var options = TestHelper.optionsFactory("add_artist_db");
            var expectedUser = new UserModel()
            {
                Id = new Guid(),
                ExternalId = 1,
                CreatedAtUtc = DateTime.UtcNow,
                IsActive = true
            };

            var message = new MessageCommand() {
                Text = "add nofx"
            };

            using (var context = new MusichinoDbContext(options))
            {
                await context.Users.AddAsync(expectedUser);
                await context.SaveChangesAsync();
            }

            //When
            using (var context = new MusichinoDbContext(options))
            {
                await service.PerformAction(Commands.Add, expectedUser.Id, message, context);
                var actualUser = await context.Users.FirstOrDefaultAsync(u => u.Id == expectedUser.Id);
                var actualArtist = await context.Artists.FirstOrDefaultAsync(a => a.Name == "nofx");
                var actualArtistUser = await context.ArtistUsers.FirstOrDefaultAsync();

                //Then
                Assert.NotEmpty(context.Artists);
                Assert.NotNull(actualArtist);
                Assert.NotNull(actualArtistUser);
                Assert.Equal("nofx", actualArtist.Name.ToLower());
                Assert.Equal(expectedUser.Id, actualArtistUser.UserId);
            }
        }

        [Fact]
        public async Task PerformActionTest_ShouldSuspendUserActivity()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var options = TestHelper.optionsFactory("suspend_user_db");
            var expectedUser = new UserModel()
            {
                Id = new Guid(),
                ExternalId = 1,
                CreatedAtUtc = DateTime.UtcNow,
                IsActive = true
            };

            using (var context = new MusichinoDbContext(options))
            {
                await context.Users.AddAsync(expectedUser);
                await context.SaveChangesAsync();
            }

            //When
            using (var context = new MusichinoDbContext(options))
            {
                await service.PerformAction(Commands.Suspend, expectedUser.Id, null, context);
                var actualUser = await context.Users.FirstOrDefaultAsync(u => u.Id == expectedUser.Id);

                //Then
                Assert.NotNull(actualUser);
                Assert.Equal(actualUser.IsActive, false);
            }
        }
    }
}