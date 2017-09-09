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

        public Commands GetMessageCommand(string message)
        {
            try
            {
                var messageDeserialiser = new { update_id = "", message = new MessageModel() };
                var messageBody = JsonConvert.DeserializeAnonymousType(message, messageDeserialiser);
                return GetCommandType(messageBody.message.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Commands GetCommandType(string text)
        {
            var indexOfFirstSpace = text.IndexOf(" ");
            var command = text.Substring(0, indexOfFirstSpace + 1).ToLower();

            if (command.Equals("add"))
            {
                return Commands.SearchArtist;
            }
            
            throw new InvalidOperationException();
        }

        public enum Commands 
        {
            SearchArtist,
            AddArtist,
            RemoveArtist
        }
    }
}