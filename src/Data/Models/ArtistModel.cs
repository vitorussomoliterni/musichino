using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musichino.Data.Models
{
    public class ArtistModel
    {
        [Key]
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string BeginYear { get; set; }
        public string EndYear { get; set; }
        public bool? Ended { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastCheckedAtUtc { get; set; }
        public string LatestReleaseName { get; set; }
        public string LatestReleaseExternalId { get; set; }
        public DateTime LatestReleaseDateUtc { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public DateTime DeletedAtUtc { get; set; }
        public Guid ArtistUserId { get; set; }
        [ForeignKey("ArtistUserId")]
        public ArtistUserModel ArtistUser { get; set; }
    }
}