using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using musichino.Data.Models;

namespace musichino.Services
{
    public class ArtistService
    {
        MusichinoDbContext _context;
        public ArtistService(MusichinoDbContext context, MusicbrainzService musicbrainz)
        {
            _context = context;
        }
    }
}