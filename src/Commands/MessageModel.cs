using System;

namespace musichino.Commands
{
    public class MessageCommand
    {
        public int MessageId { get; set; }
        public int ExternalUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public DateTime UtcDate { get; set; }
        public string Text { get; set; }
    }
}