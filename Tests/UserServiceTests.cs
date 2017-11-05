using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using musichino.Commands;
using musichino.Data.Models;
using musichino.Services;
using Xunit;

namespace Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetUserTest_ShouldGetUserFromDb()
        {
            // Given
            var options = TestHelper.optionsFactory("get_user_db");
            var expectedUser = new UserModel()
            {
                Id = new Guid(),
                FirstName = "Steven",
                LastName = "Universe",
                Username = "Zucchini",
                ExternalId = 1,
                CreatedAtUtc = DateTime.UtcNow,
                IsActive = true
            };

            using (var context = new MusichinoDbContext(options))
            {
                await context.Users.AddAsync(expectedUser);
                await context.SaveChangesAsync();
            }

            // When
            using (var context = new MusichinoDbContext(options))
            {
                var service = new UserService(context);
                var actualUser = await service.GetUser(expectedUser.ExternalId);
                
                // Then
                Assert.NotNull(actualUser);
                Assert.Equal(expectedUser.Id, actualUser.Id);
                Assert.Equal(expectedUser.FirstName, actualUser.FirstName);
                Assert.Equal(expectedUser.LastName, actualUser.LastName);
                Assert.Equal(expectedUser.Username, actualUser.Username);
                Assert.Equal(expectedUser.ExternalId, actualUser.ExternalId);
                Assert.Equal(expectedUser.CreatedAtUtc, actualUser.CreatedAtUtc);
                Assert.Equal(expectedUser.IsActive, actualUser.IsActive);
            }
        }

        [Fact]
        public async Task AddUserTest_ShouldAddUserToDb()
        {
            // Given
            var options = TestHelper.optionsFactory("add_user_db");
            var expectedUser = new UserModel()
            {
                Id = new Guid(),
                FirstName = "Steven",
                LastName = "Universe",
                Username = "Zucchini",
                ExternalId = 1,
                CreatedAtUtc = DateTime.UtcNow,
                IsActive = true
            };
            var message = new MessageCommand() {
                FirstName = expectedUser.FirstName,
                LastName = expectedUser.LastName,
                Username = expectedUser.Username,
                ExternalUserId = expectedUser.ExternalId
            };

            // When
            using (var context = new MusichinoDbContext(options))
            {
                var service = new UserService(context);
                await service.AddUser(message);
                var actualUser = await context.Users.FirstOrDefaultAsync();

                // Then
                Assert.NotEmpty(context.Users);
                Assert.NotNull(actualUser);
                Assert.Equal(expectedUser.FirstName, actualUser.FirstName);
                Assert.Equal(expectedUser.LastName, actualUser.LastName);
                Assert.Equal(expectedUser.Username, actualUser.Username);
                Assert.Equal(expectedUser.ExternalId, actualUser.ExternalId);
                Assert.Equal(expectedUser.IsActive, actualUser.IsActive);
            }
        }
    }
}