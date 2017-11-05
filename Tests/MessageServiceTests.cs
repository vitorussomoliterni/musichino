using System;
using Xunit;
using musichino.Commands;
using musichino.Services;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using musichino.Data.Models;

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
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                // Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
                var text = "add nofx";
                var rawMessage = TestHelper.rawMessageFactory(text);
                var messageModel = messageModelFactory(text);

                //When
                var actualText = service.GetMessageCommand(messageModel.Text);
                var expectedText = MessageService.Commands.Search;

                //Then
                Assert.Equal(expectedText, actualText);
            }
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnRemove()
        {
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                //Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
                var text = "remove green day";
                var rawMessage = TestHelper.rawMessageFactory(text);
                var messageModel = messageModelFactory(text);

                //When
                var actualText = service.GetMessageCommand(messageModel.Text);
                var expectedText = MessageService.Commands.Remove;

                //Then
                Assert.Equal(expectedText, actualText);
            }
        }
        
        [Fact]
        public void TestGetMessageCommand_ShouldReturnSuspend()
        {
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                //Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
                var text = "suspend";
                var rawMessage = TestHelper.rawMessageFactory(text);
                var messageModel = messageModelFactory(text);

                //When
                var actualText = service.GetMessageCommand(messageModel.Text);
                var expectedText = MessageService.Commands.Suspend;

                //Then
                Assert.Equal(expectedText, actualText);
            }
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnHelp()
        {
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                //Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
                var text = "help";
                var rawMessage = TestHelper.rawMessageFactory(text);
                var messageModel = messageModelFactory(text);

                //When
                var actualText = service.GetMessageCommand(messageModel.Text);
                var expectedText = MessageService.Commands.Help;

                //Then
                Assert.Equal(expectedText, actualText);
            }
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnList()
        {
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                //Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
                var text = "list";
                var rawMessage = TestHelper.rawMessageFactory(text);
                var messageModel = messageModelFactory(text);

                //When
                var actualText = service.GetMessageCommand(messageModel.Text);
                var expectedText = MessageService.Commands.List;

                //Then
                Assert.Equal(expectedText, actualText);
            }
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnReactivate()
        {
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                //Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
                var text = "reactivate";
                var rawMessage = TestHelper.rawMessageFactory(text);
                var messageModel = messageModelFactory(text);

                //When
                var actualText = service.GetMessageCommand(messageModel.Text);
                var expectedText = MessageService.Commands.Reactivate;

                //Then
                Assert.Equal(expectedText, actualText);
            }
        }

        [Fact]
        public void TestGetMessageCommand_ShouldThrowInvalidDataException()
        {
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                //Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);

                //Then
                Assert.Throws<InvalidDataException>(() => service.GetMessageCommand("   "));
                Assert.Throws<InvalidDataException>(() => service.GetMessageCommand(""));
                Assert.Throws<InvalidDataException>(() => service.GetMessageCommand(null));
                Assert.Throws<InvalidDataException>(() => service.GetMessageCommand(" "));
            }
        }

        [Fact]
        public void TestGetMessageCommand_ShouldReturnOther()
        {
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                //Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
                var text = "avada kevadra";
                var rawMessage = TestHelper.rawMessageFactory(text);
                var messageModel = messageModelFactory(text);

                //When
                var actualText = service.GetMessageCommand(messageModel.Text);
                var expectedText = MessageService.Commands.Other;

                //Then
                Assert.Equal(expectedText, actualText);
            }
        }

        [Fact]
        public void TestGetMessage_ShouldGetTheMessageText()
        {
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                //Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
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
        }

        // TODO: Add testing for reading the request
        [Fact]
        public async Task TestReadRequestBodyAsync_ShouldReturnString()
        {
            var options = TestHelper.optionsFactory("add_artist_db");
            
            using (var context = new MusichinoDbContext(options))
            {
                //Given
                var userService = new UserService(context);
                var musicbrainzService = new MusicbrainzService();
                var service = new MessageService(userService, musicbrainzService);
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
}