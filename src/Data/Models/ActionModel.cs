using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using musichino.Services;

namespace musichino.Data.Models
{
    public class ActionModel
    {
        [Key]
        public Guid Id { get; set; }
        public int ActionNumber { get; set; }
        public string ActionText { get; set; }
        [ForeignKey("UserId")]
        public UserModel User { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime DeletedAtUtc { get; set; }
    }
}