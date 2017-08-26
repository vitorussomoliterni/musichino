using Newtonsoft.Json;
using musichino.Models;

namespace musichino.Services
{
    public class MessageService
    {
        public string GetMessageText(string message)
        {
            var messageDeserialiser = new { update_id = "", message = "" };
            var messageBody = JsonConvert.DeserializeAnonymousType(message, messageDeserialiser);
            return messageBody.message;
        }
    }
}