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
        MusichinoDbContext _context;
        public UserService(MusichinoDbContext context)
        {
            _context = context;
        }
        public async Task<UserModel> GetUser(int? ExternalId)
        {
            if (ExternalId == null)
            {
                throw new InvalidDataException("No external user id provided");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.ExternalId == ExternalId);

            return user;
        }

        public async Task<UserModel> AddUser(MessageCommand message)
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

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        internal async Task<UserModel> suspendUser(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException($"No user found for user id {userId}");
            }

            if (!user.IsActive)
            {
                throw new InvalidOperationException($"User with id {userId} is already inactive");
            }

            user.IsActive = false;
            
            await _context.SaveChangesAsync();

            return user;
        }
    }
}