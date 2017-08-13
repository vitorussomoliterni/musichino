using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using musichino.Commands;

namespace musichino.Services
{
    public class MusicbrainzService
    {
        public async Task<IEnumerable<ArtistCommand>> GetArtistNameList(string artistName)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://musicbrainz.org");
                    client.DefaultRequestHeaders.Add("User-Agent", "Musichino_Bot/0.1 ( https://github.com/vitorussomoliterni/musichino/ )");

                    // var response = await client.GetAsync("/ws/2/artist?query=" + artistName);
                    
                    // response.EnsureSuccessStatusCode();

                    // var stringResponse = await response.Content.ReadAsStringAsync();

                    var artists = ReadXmlResponse(GetRawdataForTesting());

                    return artists;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return null;
            }
        }

        private IEnumerable<ArtistCommand> ReadXmlResponse(string body)
        {
            var doc = new XmlDocument();
            doc.LoadXml(body);

            var artists = new List<ArtistCommand>();

            var nodes = doc.DocumentElement.GetElementsByTagName("artist");

            foreach (XmlNode a in nodes)
            {
                var artist = new ArtistCommand();

                // artist.Id = a["id"].InnerText;
                // artist.Type = a["type"].InnerText;
                artist.Name = (a["name"] != null) ? a["name"].InnerText : null;
                artist.Country = (a["country"] != null) ? a["country"].InnerText : null;
                // artist.BeginYear = a["life-span"].InnerText;

                // if (a["life-span"].InnerText.Equals("false"))
                // {
                //     artist.Ended = false;
                //     artist.EndYear = null;
                // }
                // else
                // {
                //     artist.Ended = true;
                // }

                artists.Add(artist);
            }

            return artists;
        }

        private string GetRawdataForTesting()
        {
            return "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><metadata created=\"2017-08-08T11:32:33.317Z\" xmlns=\"http://musicbrainz.org/ns/mmd-2.0#\" xmlns:ext=\"http://musicbrainz.org/ns/ext#-2.0\"><artist-list count=\"2\" offset=\"0\"><artist id=\"dcaa4f81-bfb7-44eb-8594-4e74f004b6e4\" type=\"Group\" ext:score=\"100\"><name>NOFX</name><sort-name>NOFX</sort-name><country>US</country><area id=\"489ce91b-6658-3307-9877-795b68554c98\"><name>United States</name><sort-name>United States</sort-name></area><begin-area id=\"1f40c6e1-47ba-4e35-996f-fe6ee5840e62\"><name>Los Angeles</name><sort-name>Los Angeles</sort-name></begin-area><life-span><begin>1983</begin><ended>false</ended></life-span><alias-list><alias sort-name=\"No FX\">No FX</alias></alias-list><tag-list><tag count=\"1\"><name>rock</name></tag><tag count=\"3\"><name>punk</name></tag><tag count=\"3\"><name>american</name></tag><tag count=\"2\"><name>punk rock</name></tag><tag count=\"1\"><name>ska punk</name></tag><tag count=\"2\"><name>usa</name></tag><tag count=\"1\"><name>hardcore punk</name></tag><tag count=\"1\"><name>américain</name></tag><tag count=\"1\"><name>pop punk</name></tag><tag count=\"2\"><name>skate punk</name></tag><tag count=\"1\"><name>los angeles</name></tag><tag count=\"1\"><name>bay area</name></tag><tag count=\"1\"><name>classic pop punk</name></tag><tag count=\"1\"><name>california punk</name></tag><tag count=\"1\"><name>classic hardcore punk</name></tag><tag count=\"1\"><name>classic punk</name></tag><tag count=\"1\"><name>los angeles punk</name></tag><tag count=\"1\"><name>bay area punk</name></tag></tag-list></artist><artist id=\"8ab0d990-40d2-4625-835d-c68a16295ee0\" type=\"Person\" ext:score=\"72\"><name>DJ NoFx</name><sort-name>NoFx, DJ</sort-name><gender>male</gender><disambiguation>dance music DJ/producer Patrick Sanders</disambiguation><life-span><ended>false</ended></life-span><alias-list><alias sort-name=\"Sanders, Patrick\">Patrick Sanders</alias></alias-list></artist></artist-list></metadata>";
        }
    }
}