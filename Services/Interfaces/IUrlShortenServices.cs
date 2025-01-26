namespace URLShortner.Services.Interfaces
{
    public interface IUrlShortenServices
    {
        string ShortenUrl(string url);
        string GetOriginalUrl(string shortUrl);
        string UpdateOriginalUrl(string shortUrl,string updatedUrl);
    }
}
