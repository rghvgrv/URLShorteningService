using System.Text.Json;
using URLShortner.Models;
using URLShortner.Services.Interfaces;

namespace URLShortner.Services
{
    public class UrlShortenServices : IUrlShortenServices
    {
        private const string StorageFile = "urlMapping.json";
        private const string BaseUrl = "https://short.ly/";

        public UrlShortenServices()
        {
            // Initialize JSON file if not exists
            if (!File.Exists(StorageFile))
            {
                File.WriteAllText(StorageFile, "[]"); // Initialize with an empty list
            }
        }

        public string ShortenUrl(string originalUrl)
        {
            string uniqueCode = GenerateUniqueCode(originalUrl);
            var urlMappings = LoadUrlMappings();

            // Add new URL mapping as an object
            urlMappings.Add(new UrlMapping { Code = uniqueCode, OriginalUrl = originalUrl });
            SaveUrlMappings(urlMappings);

            return BaseUrl + uniqueCode;
        }

        public string GetOriginalUrl(string shortUrl)
        {
            string uniqueCode = shortUrl.Replace(BaseUrl, "");
            var urlMappings = LoadUrlMappings();

            // Find the mapping with the given code
            var mapping = urlMappings.FirstOrDefault(m => m.Code == uniqueCode);
            return mapping != null ? mapping.OriginalUrl : "URL not found.";
        }

        public string UpdateOriginalUrl(string shortUrl, string updatedOriginalUrl)
        {
            string uniqueCode = shortUrl.Replace(BaseUrl, "");
            var urlMappings = LoadUrlMappings();
            // Find the mapping with the given code
            var mapping = urlMappings.FirstOrDefault(m => m.Code == uniqueCode);
            if (mapping != null)
            {
                mapping.OriginalUrl = updatedOriginalUrl;
                SaveUrlMappings(urlMappings);
                return "URL updated successfully.";
            }
            else
            {
                return "URL not found.";
            }
        }

        private string GenerateUniqueCode(string url)
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .Substring(0, 8)
                .Replace("/", "_")
                .Replace("+", "-");
        }

        private List<UrlMapping> LoadUrlMappings()
        {
            string json = File.ReadAllText(StorageFile);
            return JsonSerializer.Deserialize<List<UrlMapping>>(json) ?? new List<UrlMapping>();
        }

        private void SaveUrlMappings(List<UrlMapping> urlMappings)
        {
            string json = JsonSerializer.Serialize(urlMappings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(StorageFile, json);
        }
    }
}
