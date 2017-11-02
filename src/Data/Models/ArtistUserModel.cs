using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musichino.Data.Models
{
    public class ArtistUserModel
    {
        [Key]
        public Guid ArtistUserId { get; set; }
        public Guid ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public ArtistModel Artist { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public UserModel User { get; set; }
    }
}

