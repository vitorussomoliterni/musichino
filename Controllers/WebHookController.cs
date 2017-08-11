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

            using (var reader = new StreamReader(Request.Body))
            {
                var text = await reader.ReadToEndAsync();

                if (text == "ciao")
                {
                    return Ok("This is working");
                }
            }

            return Unauthorized();
        }
    }
}
