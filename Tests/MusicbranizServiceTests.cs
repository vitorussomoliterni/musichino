using System;
using Xunit;
using musichino.Models;
using musichino.Services;
using System.Threading.Tasks;
using System.Net.Http;

namespace Tests
{
    public class MusicbrainzServiceTests
    {
        [Fact]
        public async Task TestGetArtistNameList_ShouldReturnEmptyList()
        {
            var service = new MusicbrainzService();

            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.GetArtistNameList(""));
        }
    }
}