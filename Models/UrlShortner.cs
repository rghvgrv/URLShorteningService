namespace URLShortner.Models
{
    public class UrlShortner
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int AccessCount { get; set; }
    }
}
