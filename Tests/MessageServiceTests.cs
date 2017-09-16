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
            var rawMessage = "{\"update_id\":10000,\"message\":{\"date\":1441645532,\"chat\":{\"last_name\":\"Test Lastname\",\"id\":1111111,\"first_name\":\"Test\",\"username\":\"Test\"},\"message_id\":1365,\"from\":{\"last_name\":\"Test Lastname\",\"id\":1111111,\"first_name\":\"Test\",\"username\":\"Test\"},\"text\":\"" + userMessage + "\"}}";
            return rawMessage;
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnAddArtist()
        {
            //Given
            var service = new MessageService();
            var rawMessage = rawMessageFactory("add nofx");

            //When
            var actualText = service.GetMessageCommand(rawMessage);
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

            //When
            var actualMessage = service.GetMessage(rawMessage);
            var expectedMessage = new MessageModel()
            {
                MessageId = 10000,
                LastName = "Test Lastname",
                FirstName = "Test",
                Username = "Test",
                Text = text
            };

            //Then
            Assert.Equal(expectedMessage, actualMessage);
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