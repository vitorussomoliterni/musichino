using Microsoft.EntityFrameworkCore;
using musichino.Data.Models;

namespace Tests
{
    internal class TestHelper
    {
        internal static DbContextOptions<MusichinoDbContext> optionsFactory(string dbName)
        {
            var options = new DbContextOptionsBuilder<MusichinoDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return options;
        }

        internal static string rawMessageFactory(string userMessage)
        {
            var rawMessage = @"{'update_id': 10000,
                                'message': {
                                    'date': 1483228800,
                                       'chat': {
                                            'last_name': 'Test Lastname',
                                            'id': 1111111,
                                            'first_name': 'Test',
                                            'username': 'Test'
                                            },
                                        'message_id': 1365,
                                        'from': {
                                            'last_name': 'Test Lastname',
                                            'id': 1111111,
                                            'first_name': 'Test',
                                            'username': 'Test'
                                            },
                                        'text':'" + userMessage + 
                                    @"'}
                                }";
            return rawMessage;
        }
    }
}