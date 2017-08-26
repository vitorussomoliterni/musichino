using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using musichino.Services;
using musichino.Data.Models;
using musichino.Models;

namespace musichino.Controllers
{
    [Route("")]
    public class WebHook : Controller
    {
        private MusicbrainzService _musicbrainz;
        private MessageService _message;

        public WebHook(MusicbrainzService musicbrainz, MessageService message)
        {
            _musicbrainz = musicbrainz;
            _message = message;
        }
        
        [HttpPost("")]
        public async Task<IActionResult> Receive()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var messageResponse = String.Empty;
                var rawMessage = await reader.ReadToEndAsync();

                if (String.IsNullOrEmpty(rawMessage) || String.IsNullOrWhiteSpace(rawMessage))
                {
                    return BadRequest();
                }

                try
                {
                    var messageBody = _message.GetMessageText(rawMessage);
                    
                    var response = await _musicbrainz.GetArtistNameList(messageBody.Text);

                    if (!response.Any())
                    {
                        return NotFound("No artist found\n");
                    }

                    foreach (var artist in response)
                    {
                        messageResponse += $"{artist.Name}, {artist.Type} ({artist.Country})\n";
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
