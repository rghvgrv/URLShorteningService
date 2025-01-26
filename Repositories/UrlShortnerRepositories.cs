using System.Text.Json;
using System.Xml.Serialization;
using URLShortner.Models;
using URLShortner.Repositories.Interfaces;
using URLShortner.Services.Interfaces;

namespace URLShortner.Repositories
{
    public class UrlShortnerRepositories : IUrlShortnerRepositories
    {
        private readonly string filePath;
        private IUrlShortenServices _urlShortenServices;
        public UrlShortnerRepositories(IUrlShortenServices urlShortenServices,string _filePath = "UrlShortner.json")
        {
            filePath = _filePath;
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
            _urlShortenServices = urlShortenServices;
        }

        public List<UrlShortner> GetAll()
        {
            if (!File.Exists(filePath))
            {
                return new List<UrlShortner>();
            }
            return JsonSerializer.Deserialize<List<UrlShortner>>(File.ReadAllText(filePath));
        }
        public void SaveURLs(List<UrlShortner> urls)
        {
            string jsonContent = JsonSerializer.Serialize(urls);
            File.WriteAllText(filePath, jsonContent);
        }

        public List<UrlShortner> GetDetailsFromURL(string shortUrl)
        {
            var urldetails = JsonSerializer.Deserialize<List<UrlShortner>>(File.ReadAllText(filePath));
            var url = _urlShortenServices.GetOriginalUrl(shortUrl);
            if (urldetails.Count > 0)
            {
                return urldetails.Where(x => x.OriginalUrl == url).ToList();
            }
            else
            {
                return new List<UrlShortner>();
            }
        }

    }
}
