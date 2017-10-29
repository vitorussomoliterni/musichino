using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using musichino.Services;
using musichino.Data.Models;
using musichino.Commands;

namespace musichino.Controllers
{
    [Route("")]
    public class WebHook : Controller
    {
        private MusicbrainzService _musicbrainz;
        private MessageService _message;
        private MusichinoDbContext _context;
        private UserService _user;

        public WebHook(MusicbrainzService musicbrainz, MessageService message, MusichinoDbContext context, UserService user)
        {
            _musicbrainz = musicbrainz;
            _message = message;
            _context = context;
            _user = user;
        }
        
        [HttpPost("")]
        public async Task<IActionResult> ReceiveMessages()
        {
            try
            {
                var rawMessage = await _message.ReadRequestBodyAsync(Request.Body);
                var message = _message.GetMessage(rawMessage);
                var action = _message.GetMessageCommand(message.Text);
                var user = await _user.GetUser(message.ExternalUserId, _context);
                
                if (user == null)
                {
                    await _user.AddUser(message, _context);
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
