using BookStoreClient.Models;
using BookStoresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Reflection;

namespace BookStoreClient.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private string BookApiUrl = "";

        public BookController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = $"https://localhost:7256/api/Book/";
        }

        public async Task<IActionResult> Index()
        {
            string url = BookApiUrl + "GetAll";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            var result = response.Content.ReadFromJsonAsync<List<BookDto>>().Result;
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BookDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string url = BookApiUrl + "Create";
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, model);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;

            if (result) { ViewData["msg"] = "Create success!"; }
            else { ViewData["msg"] = "Create failed!"; }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(string? id)
        {
            var book = await GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(BookDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string url = BookApiUrl + "Update";
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(url, model);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;

            if (result) { ViewData["msg"] = "Update success!"; }
            else { ViewData["msg"] = "Update failed!"; }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            var book = await GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            string url = BookApiUrl + $"Delete/{id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;

            if (result) { TempData["msg"] = "Delete Success!"; }
            else { TempData["msg"] = "Delete failed!"; }

            return RedirectToAction("Index");
        }

        private async Task<BookDto> GetByIdAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            string url = BookApiUrl + $"GetAll?$filter=id eq '{id}'";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            var result = response.Content.ReadFromJsonAsync<List<BookDto>>().Result;

            if (result == null || result.Count == 0) return null;

            return new BookDto
            {
                Id = result[0].Id,
                IBSN = result[0].IBSN,
                Title = result[0].Title,
                Author = result[0].Author,
                Price = result[0].Price,
                City = result[0].City,
                Street = result[0].Street,
            };
        }
    }
}
