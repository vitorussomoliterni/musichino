using System;
using Xunit;
using musichino.Models;
using musichino.Services;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.IO;

namespace Tests
{
    public class MessageServiceTests
    {
        private string rawMessageFactory(string userMessage)
        {
            var rawMessage = "{\"update_id\":10000,\"message\":{\"date\":1483228800,\"chat\":{\"last_name\":\"Test Lastname\",\"id\":1111111,\"first_name\":\"Test\",\"username\":\"Test\"},\"message_id\":1365,\"from\":{\"last_name\":\"Test Lastname\",\"id\":1111111,\"first_name\":\"Test\",\"username\":\"Test\"},\"text\":\"" + userMessage + "\"}}";
            return rawMessage;
        }

        private MessageModel messageModelFactory(string text)
        {
            var messageModel = new MessageModel()
            {
                MessageId = 1365,
                UserId = 1111111,
                UtcDate = new DateTime(2017, 1, 1),
                LastName = "Test Lastname",
                FirstName = "Test",
                Username = "Test",
                Text = text
            };

            return messageModel;
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnAddArtist()
        {
            //Given
            var service = new MessageService();
            var messageModel = messageModelFactory("add nofx");
            var rawMessage = rawMessageFactory("add nofx");

            //When
            var actualText = service.GetMessageCommand(messageModel.Text);
            var expectedText = MessageService.Commands.Add;

            //Then
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void TestGetMessage_ShouldGetTheMessageText()
        {
            //Given
            var service = new MessageService();
            var text = "add nofx";
            var rawMessage = rawMessageFactory(text);
            var messageModel = messageModelFactory("add nofx");

            //When
            var actualMessage = service.GetMessage(rawMessage);
            var expectedMessage = messageModel;

            //Then
            Assert.NotNull(actualMessage);
            Assert.Equal(expectedMessage.UtcDate, actualMessage.UtcDate);
            Assert.Equal(expectedMessage.UserId, actualMessage.UserId);
            Assert.Equal(expectedMessage.FirstName, actualMessage.FirstName);
            Assert.Equal(expectedMessage.LastName, actualMessage.LastName);
            Assert.Equal(expectedMessage.MessageId, actualMessage.MessageId);
            Assert.Equal(expectedMessage.Text, actualMessage.Text);
            Assert.Equal(expectedMessage.Username, actualMessage.Username);
        }

        // TODO: Add testing for reading the request
        /*
        [Fact]
        public async Task TestReadRequestBodyAsync_ShouldReturnString()
        {
            //Given
            var service = new MessageService();
            var message = messageFactory("add nofx");
            var stream = new byte[255];
            
            //When
            using (var writer = new StreamWriter(new MemoryStream(stream)))
            {
                writer.WriteLine(message);
            }
            var result = await service.ReadRequestBodyAsync(stream);
            
            //Then
            Assert.Equal();
        }
        */
    }
}