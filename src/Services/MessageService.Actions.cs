using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using musichino.Data.Models;

namespace musichino.Services
{
    public partial class MessageService
    {
        readonly ILogger<MessageService> _logger;
        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }
        public async Task PerformAction(Commands action, Guid UserId, MusichinoDbContext context)
        {
            switch (action)
            {
                case Commands.Add:
                    // Let's add
                    break;
                case Commands.Help:
                    // Let's help
                    break;
                case Commands.List:
                    // Let's list
                    break;
                case Commands.Other:
                    // Let's other
                    break;
                case Commands.Reactivate:
                    // Let's reactivate
                    break;
                case Commands.Remove:
                    // Let's remove
                    break;
                case Commands.Suspend:
                    // Let's suspend
                    break;
                default:
                    // Why are you even here
                    throw new InvalidOperationException();
            }
        }
    }
}