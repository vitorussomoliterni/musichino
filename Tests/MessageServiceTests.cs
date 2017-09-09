using System;
using Xunit;
using musichino.Models;
using musichino.Services;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using static musichino.Services.MessageService;

namespace Tests
{
    public class MessageServiceTests
    {
        private string MessageFactory(string text)
        {
            var message = "{\"update_id\":10000,\"message\":{\"date\":1441645532,\"chat\":{\"last_name\":\"Test Lastname\",\"id\":1111111,\"first_name\":\"Test\",\"username\":\"Test\"},\"message_id\":1365,\"from\":{\"last_name\":\"Test Lastname\",\"id\":1111111,\"first_name\":\"Test\",\"username\":\"Test\"},\"text\":\"" + text + "\"}}";
            return message;
        }
        [Fact]
        public void TestGetMessageCommand_ShouldReturnAddArtist()
        {
            var service = new MessageService();
            var message = MessageFactory("add nofx");

            var result = service.GetMessageCommand(message);

            Assert.Equal(Commands.Add, result);
        }
    }
}