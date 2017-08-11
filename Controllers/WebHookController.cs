using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace musichino.Controllers
{
    [Route("[controller]")]
    public class WebHook : Controller
    {
        [HttpPost("")]
        public async Task<IActionResult> Receive()
        {
            Request.Headers.TryGetValue("X-GitHub-Event", out StringValues eventName);
            Request.Headers.TryGetValue("X-Hub-Signature", out StringValues signature);
            Request.Headers.TryGetValue("X-GitHub-Delivery", out StringValues delivery);

            using (var reader = new StreamReader(Request.Body))
            {
                var text = await reader.ReadToEndAsync();

                if (IsRequestAllowed(text, eventName, signature))
                {
                    return Ok("This is working");
                }
            }

            return Unauthorized();
        }

        private bool IsRequestAllowed(string payload, StringValues eventName, StringValues signatureWithPrefix)
        {
            if (payload == "ciao")
            {
                return true;
            }
            
            return false;
        }
    }
}
