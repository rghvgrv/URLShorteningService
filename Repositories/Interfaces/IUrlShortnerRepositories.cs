using URLShortner.Models;

namespace URLShortner.Repositories.Interfaces
{
    public interface IUrlShortnerRepositories
    {
        List<UrlShortner> GetAll();
        void SaveURLs(List<UrlShortner> urls);

        List<UrlShortner> GetDetailsFromURL(string url);
    }
}
