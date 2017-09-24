using System;
using musichino.Data.Models;

namespace musichino.Services
{
    public partial class MessageService
    {
        public void PerformAction(Commands action, int UserId)
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