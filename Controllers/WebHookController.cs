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
    [Route("")]
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

                if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text))
                {
                    return BadRequest("Specify an artist name\n");
                }

                try
                {
                    var response = await _musicbrainz.GetArtistNameList(text);

                    if (!response.Any())
                    {
                        return NotFound("No artist found\n");
                    }

                    foreach (var artist in response)
                    {
                        messageResponse += $"{artist.Name}, {artist.Type} ({artist.Country} - {artist.BeginYear} to {artist.EndYear}) {artist.Ended}\n";
                    }

                    return Ok(messageResponse);
                }
                catch (System.Exception ex)
                {
                    return StatusCode(500, ex);
                }
            }
        }
    }
}
