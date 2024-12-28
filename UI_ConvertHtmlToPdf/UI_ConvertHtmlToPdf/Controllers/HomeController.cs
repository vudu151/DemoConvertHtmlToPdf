using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using UI_ConvertHtmlToPdf.Models;

namespace UI_ConvertHtmlToPdf.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public HomeController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConvertToPdf(string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent))
            {
                ModelState.AddModelError("htmlContent", "Html content is required");
                return View("Index");
            }

            try
            {
                HttpClient client = _httpClient.CreateClient();
                string jsonContent = JsonConvert.SerializeObject(new { htmlContent });
                StringContent requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://localhost:5555/api/Pdf/convert", requestContent);
                if (response.IsSuccessStatusCode)
                {
                    byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();
                    return File(pdfBytes, "application/pdf", "converted.pdf");
                }
                else
                {
                    ViewBag.Error = "Failed to convert HTML to PDF.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error: {ex.Message}";
                return View("Index");
            }

        }
    }
}
