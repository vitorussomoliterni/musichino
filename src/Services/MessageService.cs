using Newtonsoft.Json;
using musichino.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace musichino.Services
{
    public class MessageService
    {
        public MessageModel GetMessage(string rawMessage)
        {
            try
            {
                var messageDeserialiser = new { message = new MessageModel() };
                var messageBody = JsonConvert.DeserializeAnonymousType(rawMessage, messageDeserialiser);
                return messageBody.message;
            }
            catch (Exception ex)
            {
                // TODO: Better error handling
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
                // TODO: Better error handling
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