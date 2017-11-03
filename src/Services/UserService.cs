using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using musichino.Data.Models;
using System.IO;
using System;
using musichino.Commands;

namespace musichino.Services
{
    public class UserService
    {
        public async Task<UserModel> GetUser(int? ExternalId, MusichinoDbContext context)
        {
            if (ExternalId == null)
            {
                throw new InvalidDataException("No external user id provided");
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.ExternalId == ExternalId);

            return user;
        }

        public async Task<UserModel> AddUser(MessageCommand message, MusichinoDbContext context)
        {
            if (message == null)
            {
                throw new InvalidDataException("No message provided");
            }

            var user = new UserModel() {
                Id = new Guid(),
                FirstName = message.FirstName,
                LastName = message.LastName,
                Username = message.Username,
                ExternalId = message.ExternalUserId,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        internal async Task<UserModel> suspendUser(Guid userId, MusichinoDbContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException($"No user found for user id {userId}");
            }

            if (!user.IsActive)
            {
                throw new InvalidOperationException($"User with id {userId} is already inactive");
            }

            user.IsActive = false;
            
            await context.SaveChangesAsync();

            return user;
        }
    }
}