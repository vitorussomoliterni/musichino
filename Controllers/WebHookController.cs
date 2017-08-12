using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using musichino.Services;

namespace musichino.Controllers
{
    [Route("[controller]")]
    public class WebHook : Controller
    {
        private Fetcher _fetcher;

        public WebHook(Fetcher fetcher)
        {
            _fetcher = fetcher;
        }
        
        [HttpPost("")]
        public async Task<IActionResult> Receive()
        {

            using (var reader = new StreamReader(Request.Body))
            {
                var text = await reader.ReadToEndAsync();

                var response = await _fetcher.GetArtistList(text);

                return Ok(response);
            }
        }
    }
}
