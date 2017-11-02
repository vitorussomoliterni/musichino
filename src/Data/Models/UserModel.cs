using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musichino.Data.Models
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int ExternalId { get; set; }
        public Guid ArtistUserId { get; set; }
        [ForeignKey("ArtistUserId")]
        public ArtistUserModel ArtistUser { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}