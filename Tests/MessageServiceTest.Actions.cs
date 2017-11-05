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
            // Given
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
                // When
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
                await context.Users.AddAsync(expectedUser);
                await context.SaveChangesAsync();

                await service.PerformAction(Commands.Search, expectedUser.Id, message);
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
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
                await context.Users.AddAsync(expectedUser);
                await context.SaveChangesAsync();

                // When
                await service.PerformAction(Commands.Suspend, expectedUser.Id, null);
                var actualUser = await context.Users.FirstOrDefaultAsync(u => u.Id == expectedUser.Id);

                //Then
                Assert.NotNull(actualUser);
                Assert.Equal(actualUser.IsActive, false);
            }
        }
    }
}