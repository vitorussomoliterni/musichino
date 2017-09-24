using System;
using System.Collections.Generic;

namespace musichino.Data.Models
{
    public class ArtistModel
    {
        public ArtistModel()
        {
            Users = new List<UserModel>();
        }
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string BeginYear { get; set; }
        public string EndYear { get; set; }
        public bool? Ended { get; set; }
        public List<UserModel> Users { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastCheckedAtUtc { get; set; }
        public string LatestReleaseName { get; set; }
        public string LatestReleaseExternalId { get; set; }
        public DateTime LatestReleaseDateUtc { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public DateTime DeletedAtUtc { get; set; }
    }
}