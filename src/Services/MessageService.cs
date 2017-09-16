using Newtonsoft.Json;
using musichino.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace musichino.Services
{
    public class MessageService
    {
        public MessageModel GetMessage(string message)
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
                return getCommandType(messageBody.message.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Commands getCommandType(string text)
        {
            var indexOfFirstSpace = text.IndexOf(" ");
            var command = text.Substring(0, indexOfFirstSpace).ToLower();

            switch (command)
            {
                case "add":
                    return Commands.Add;
                case "remove":
                    return Commands.Remove;
                case "suspend":
                    return Commands.Suspend;
                case "help":
                    return Commands.Help;
                default:
                    return Commands.Other;
            }
            
            throw new InvalidOperationException();
        }

        public async Task<string> ReadRequestBodyAsync(Stream body)
        {
            var rawMessage = String.Empty;
            using (var reader = new StreamReader(body))
            {
                rawMessage = await reader.ReadToEndAsync();
            }

            if (String.IsNullOrEmpty(rawMessage) || String.IsNullOrWhiteSpace(rawMessage))
            {
                // TODO: Handle this exception properly
                throw new Exception("No request found");
            }

            return rawMessage;
        }

        private void performAction(int command)
        {
            switch (command)
            {
                
            }
        }

        public enum Commands 
        {
            Add,
            Remove,
            Suspend,
            Help,
            Other
        }
    }
}