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
                var messageDeserialiser = new MessageJsonModel();
                var messageBody = JsonConvert.DeserializeAnonymousType(rawMessage, messageDeserialiser);
                var message = mapJsonModelToMessage(messageBody);

                return message;
            }
            catch (Exception ex)
            {
                // TODO: Better error handling
                throw ex;
            }
        }

        private MessageModel mapJsonModelToMessage(MessageJsonModel body)
        {
            var message = new MessageModel() {
                MessageId = body.MessageId,
                UserId = body.Sender.UserId,
                Date = DateTimeService.UnixTimeToDateTime(body.Date),
                FirstName = body.Sender.FirstName,
                LastName = body.Sender.LastName,
                Username = body.Sender.Username,
                Text = body.Text
            };

            return message;
        }

        public Commands GetMessageCommand(string text)
        {
            try
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
            catch (Exception ex)
            {
                // TODO: Better error handling
                throw ex;
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

    internal class MessageJsonModel
    {
        [JsonProperty("message_id")]
        internal int MessageId { get; set; }
        internal int Date { get; set; }
        [JsonProperty("from")]
        internal SenderJsonModel Sender { get; set; }
        internal string Text { get; set; }
    }

    internal class SenderJsonModel
    {
        [JsonProperty("id")]
        internal int UserId { get; set; }
        [JsonProperty("first_name")]
        internal string FirstName { get; set; }
        [JsonProperty("last_name")]
        internal string LastName { get; set; }
        internal string Username { get; set; }
    }
}