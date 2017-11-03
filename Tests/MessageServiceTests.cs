using System;
using Xunit;
using musichino.Commands;
using musichino.Services;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace Tests
{
    public partial class MessageServiceTests
    {
        private MessageCommand messageModelFactory(string text)
        {
            var messageModel = new MessageCommand()
            {
                MessageId = 1365,
                ExternalUserId = 1111111,
                UtcDate = new DateTime(2017, 1, 1),
                LastName = "Test Lastname",
                FirstName = "Test",
                Username = "Test",
                Text = text
            };

            return messageModel;
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnAdd()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var text = "add nofx";
            var rawMessage = TestHelper.rawMessageFactory(text);
            var messageModel = messageModelFactory(text);

            //When
            var actualText = service.GetMessageCommand(messageModel.Text);
            var expectedText = MessageService.Commands.Add;

            //Then
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnRemove()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var text = "remove green day";
            var rawMessage = TestHelper.rawMessageFactory(text);
            var messageModel = messageModelFactory(text);

            //When
            var actualText = service.GetMessageCommand(messageModel.Text);
            var expectedText = MessageService.Commands.Remove;

            //Then
            Assert.Equal(expectedText, actualText);
        }
        
        [Fact]
        public void TestGetMessageCommand_ShouldReturnSuspend()
        {

            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var text = "suspend";
            var rawMessage = TestHelper.rawMessageFactory(text);
            var messageModel = messageModelFactory(text);

            //When
            var actualText = service.GetMessageCommand(messageModel.Text);
            var expectedText = MessageService.Commands.Suspend;

            //Then
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnHelp()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var text = "help";
            var rawMessage = TestHelper.rawMessageFactory(text);
            var messageModel = messageModelFactory(text);

            //When
            var actualText = service.GetMessageCommand(messageModel.Text);
            var expectedText = MessageService.Commands.Help;

            //Then
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnList()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var text = "list";
            var rawMessage = TestHelper.rawMessageFactory(text);
            var messageModel = messageModelFactory(text);

            //When
            var actualText = service.GetMessageCommand(messageModel.Text);
            var expectedText = MessageService.Commands.List;

            //Then
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnReactivate()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var text = "reactivate";
            var rawMessage = TestHelper.rawMessageFactory(text);
            var messageModel = messageModelFactory(text);

            //When
            var actualText = service.GetMessageCommand(messageModel.Text);
            var expectedText = MessageService.Commands.Reactivate;

            //Then
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void TestGetMessageCommand_ShouldThrowInvalidDataException()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);

            //Then
            Assert.Throws<InvalidDataException>(() => service.GetMessageCommand("   "));
            Assert.Throws<InvalidDataException>(() => service.GetMessageCommand(""));
            Assert.Throws<InvalidDataException>(() => service.GetMessageCommand(null));
            Assert.Throws<InvalidDataException>(() => service.GetMessageCommand(" "));
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnOther()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var text = "avada kevadra";
            var rawMessage = TestHelper.rawMessageFactory(text);
            var messageModel = messageModelFactory(text);

            //When
            var actualText = service.GetMessageCommand(messageModel.Text);
            var expectedText = MessageService.Commands.Other;

            //Then
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void TestGetMessage_ShouldGetTheMessageText()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var text = "add nofx";
            var rawMessage = TestHelper.rawMessageFactory(text);
            var messageModel = messageModelFactory(text);

            //When
            var actualMessage = service.GetMessage(rawMessage);
            var expectedMessage = messageModel;

            //Then
            Assert.NotNull(actualMessage);
            Assert.Equal(expectedMessage.UtcDate, actualMessage.UtcDate);
            Assert.Equal(expectedMessage.ExternalUserId, actualMessage.ExternalUserId);
            Assert.Equal(expectedMessage.FirstName, actualMessage.FirstName);
            Assert.Equal(expectedMessage.LastName, actualMessage.LastName);
            Assert.Equal(expectedMessage.MessageId, actualMessage.MessageId);
            Assert.Equal(expectedMessage.Text, actualMessage.Text);
            Assert.Equal(expectedMessage.Username, actualMessage.Username);
        }

        // TODO: Add testing for reading the request
        [Fact]
        public async Task TestReadRequestBodyAsync_ShouldReturnString()
        {
            //Given
            var userService = new UserService();
            var service = new MessageService(userService);
            var rawMessage = TestHelper.rawMessageFactory("add nofx");
            
            //When
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(rawMessage)))
            {
                var result = await service.ReadRequestBodyAsync(stream);
            
                //Then
                Assert.Equal(rawMessage, result);
            }
        }
    }
}