using Newtonsoft.Json;
using musichino.Models;
using System;

namespace musichino.Services
{
    public class MessageService
    {
        public MessageModel GetMessageText(string message)
        {
            try
            {
                var messageDeserialiser = new { update_id = "", message = new MessageModel() };
                var messageBody = JsonConvert.DeserializeAnonymousType(message, messageDeserialiser);
                return messageBody.message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}