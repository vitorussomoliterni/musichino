using System;
using System.Collections.Generic;

namespace musichino.Data.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Artists = new List<ArtistModel>();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string ExternalId { get; set; }
        public List<ArtistModel> Artists { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}