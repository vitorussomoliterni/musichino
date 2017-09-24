using System;
using musichino.Services;

namespace musichino.Data.Models
{
    public class ActionModel
    {
        public Guid Id { get; set; }
        public int ActionNumber { get; set; }
        public string ActionText { get; set; }
        public UserModel User { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime DeletedAtUtc { get; set; }
    }
}