using Microsoft.AspNetCore.Mvc;
using URLShortner.Models;
using URLShortner.Repositories.Interfaces;
using URLShortner.Services.Interfaces;

namespace URLShortner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class URLShortnerController : ControllerBase
    {
        private IUrlShortnerRepositories _urlShortnerRepositories;
        private IUrlShortenServices _urlShortenServices;
        private static int idCounter = 1;
        public URLShortnerController(IUrlShortnerRepositories urlShortnerRepositories,IUrlShortenServices urlShortenServices)
        {
            _urlShortnerRepositories = urlShortnerRepositories;
            _urlShortenServices = urlShortenServices;
        }

        [HttpGet]
        [Route("/Get/{url}")]
        public IActionResult Get(string url)
        {
            return Ok(_urlShortnerRepositories.GetDetailsFromURL(url));
        }

        [HttpGet]
        [Route("/GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_urlShortnerRepositories.GetAll());
        }

        [HttpPost]
        [Route("/shorten")]
        public IActionResult ShortenURL([FromBody] string url)
        {
            var urls = _urlShortnerRepositories.GetAll();
            int  id = urls.Count > 0 ? urls.Max(t => t.Id) + 1 : idCounter;
            UrlShortner uRLShortners = new UrlShortner
            {
                Id = id,
                OriginalUrl = url,
                ShortenUrl = _urlShortenServices.ShortenUrl(url),
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
            urls.Add(uRLShortners);
            _urlShortnerRepositories.SaveURLs(urls);
            return Ok(uRLShortners);
        }

        [HttpPut]
        [Route("/update/{url}")]
        public IActionResult UpdateURL(string url,[FromBody] string updateOriginalUrl)
        {
            var originalUrl = _urlShortenServices.GetOriginalUrl(url);
            var updateurl = _urlShortenServices.UpdateOriginalUrl(url, updateOriginalUrl);
            var urls = _urlShortnerRepositories.GetAll();
            var urlDetails = urls.FirstOrDefault(x => x.OriginalUrl == originalUrl);

            if (urlDetails != null)
            {
                urlDetails.OriginalUrl = updateOriginalUrl;
                urlDetails.ShortenUrl = url;
                urlDetails.LastModifiedDate = DateTime.Now;
                _urlShortnerRepositories.SaveURLs(urls);
                return Ok(urlDetails);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("/delete/{url}")]
        public IActionResult DeleteURL(string url)
        {
            var urls = _urlShortnerRepositories.GetAll();
            var originalurl = _urlShortenServices.GetOriginalUrl(url);
            var urlDetails = urls.FirstOrDefault(x => x.OriginalUrl == originalurl);
            if (urlDetails != null)
            {
                urls.Remove(urlDetails);
                _urlShortnerRepositories.SaveURLs(urls);
                return Ok($"{url} - is deleted");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
