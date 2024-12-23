using ConvertHtmlToPdf.Dtos;
using ConvertHtmlToPdf.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConvertHtmlToPdf.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : Controller
    {
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost("convert")]
        public IActionResult ConvertHtmlToPdf([FromBody] HtmlContentModel htmlContent)
        {
            var pdf = _pdfService.ConvertHtmlToPdf(htmlContent.HtmlContent);
            return File(pdf, "application/pdf", "document.pdf");
        }
    }
}
