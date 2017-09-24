using Newtonsoft.Json;
using musichino.Commands;
using System;
using System.IO;
using System.Threading.Tasks;

namespace musichino.Services
{
    public partial class MessageService
    {
        public MessageCommand GetMessage(string rawMessage)
        {
            try
            {
                var messageBody = JsonConvert.DeserializeObject<IncomingUpdateJsonModel>(rawMessage);
                var message = mapJsonModelToMessage(messageBody.Message);

                return message;
            }
            catch (Exception ex)
            {
                // TODO: Better error handling
                throw ex;
            }
        }

        public Commands GetMessageCommand(string text)
        {
            try
            {
                if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text))
                {
                    throw new InvalidDataException();
                }
                var command = getCommandFromText(text);

                switch (command)
                {
                    case "add":
                        return Commands.Add;
                    case "help":
                        return Commands.Help;
                    case "list":
                        return Commands.List;
                    case "reactivate":
                        return Commands.Reactivate;
                    case "remove":
                        return Commands.Remove;
                    case "suspend":
                        return Commands.Suspend;
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

        private string getCommandFromText(string text)
        {
            text = text.Trim().ToLower();

            var indexOfFirstSpace = text.IndexOf(" ");

            if (indexOfFirstSpace <= 0)
            {
                return text;
            }

            return text.Substring(0, indexOfFirstSpace);
        }

        public async Task<string> ReadRequestBodyAsync(Stream body)
        {
            try
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
            catch (Exception ex)
            {
                // TODO: Better error handling
                throw ex;
            }
        }

        private MessageCommand mapJsonModelToMessage(MessageJsonModel body)
        {
            var message = new MessageCommand() {
                MessageId = body.MessageId,
                ExternalUserId = body.Sender.UserId,
                UtcDate = DateTimeHelper.UnixTimeToDateTime(body.Date),
                FirstName = body.Sender.FirstName,
                LastName = body.Sender.LastName,
                Username = body.Sender.Username,
                Text = body.Text
            };

            return message;
        }

        public enum Commands 
        {
            Add,
            Help,
            List,
            Other,
            Reactivate,
            Remove,
            Suspend
        }
    }

    public class IncomingUpdateJsonModel
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