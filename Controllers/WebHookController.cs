using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using musichino.Services;
using musichino.Data.Models;

namespace musichino.Controllers
{
    [Route("[controller]")]
    public class WebHook : Controller
    {
        private MusicbrainzService _musicbrainz;

        public WebHook(MusicbrainzService musicbrainz)
        {
            _musicbrainz = musicbrainz;
        }
        
        [HttpPost("")]
        public async Task<IActionResult> Receive()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var messageResponse = String.Empty;
                var text = await reader.ReadToEndAsync();
                var response = await _musicbrainz.GetArtistNameList(text);

                foreach (var artist in response)
                {
                    messageResponse += $"{artist.Id} - {artist.Name}, {artist.Type} ({artist.Country} - {artist.BeginYear})\n";
                }

                if (!response.Any())
                {
                    return NotFound("Not found\n");
                }

                return Ok(messageResponse);
            }
        }
    }
}
