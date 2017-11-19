using System;
using System.Threading.Tasks;
using musichino.Commands;

namespace musichino.Services
{
    public partial class MessageService
    {
        UserService _user;
        MusicbrainzService _musicbrainz;
        public MessageService(UserService user, MusicbrainzService musicbrainz)
        {
            _user = user;
            _musicbrainz = musicbrainz;
        }
        public async Task PerformAction(Commands action, Guid userId, MessageCommand message)
        {
            switch (action)
            {
                case Commands.Search:
                    // Let's search
                    break;
                case Commands.Help:
                    // Let's help
                    break;
                case Commands.List:
                    // Let's list
                    break;
                case Commands.Other:
                    // Let's other
                    break;
                case Commands.Reactivate:
                    // Let's reactivate
                    break;
                case Commands.Remove:
                    // Let's remove
                    break;
                case Commands.Suspend:
                    await _user.suspendUser(userId);
                    break;
                default:
                    // How are you even here?
                    throw new InvalidOperationException("No action found");
            }
        }
    }
}