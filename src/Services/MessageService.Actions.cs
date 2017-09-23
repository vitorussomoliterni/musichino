using System;

namespace musichino.Services
{
    public partial class MessageService
    {
        public void PerformAction(Commands action)
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
                case Commands.Remove:
                    // Let's remove
                case Commands.Suspend:
                    // Let's suspend
                default:
                    // Why are you even here
                    throw new InvalidOperationException();
            }
        }
    }
}