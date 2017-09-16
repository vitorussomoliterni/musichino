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