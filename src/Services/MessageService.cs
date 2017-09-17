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
                var messageBody = JsonConvert.DeserializeObject<UpdateJsonModel>(rawMessage);
                var message = mapJsonModelToMessage(messageBody.Message);

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

    public class UpdateJsonModel
    {
        public MessageJsonModel Message { get; set; }
    }

    public class MessageJsonModel
    {
        [JsonProperty("message_id")]
        public int MessageId { get; set; }
        public int Date { get; set; }
        [JsonProperty("from")]
        public SenderJsonModel Sender { get; set; }
        public string Text { get; set; }
    }

    public class SenderJsonModel
    {
        [JsonProperty("id")]
        public int UserId { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}