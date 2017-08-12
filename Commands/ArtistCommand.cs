namespace musichino.Commands
{
    public class ArtistCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string BeginYear { get; set; }
        public string EndYear { get; set; }
        public bool? Ended { get; set; }
    }
}