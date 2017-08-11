using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace musichino.Services
{
public class Fetcher
{
    public async Task<string> GetArtistList(string artistName)
    {
        using (var client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri("https://musicbrainz.org");
                client.DefaultRequestHeaders.Add("User-Agent", "Musichino_Bot/0.1 ( https://github.com/vitorussomoliterni/musichino/ )");

                var response = await client.GetAsync("/ws/2/artist?query=" + artistName);


                response.EnsureSuccessStatusCode();

                var stringResponse = await response.Content.ReadAsStringAsync();

                return stringResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
}