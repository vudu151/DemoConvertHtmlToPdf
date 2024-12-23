namespace ConvertHtmlToPdf.Services
{
    public interface IPdfService
    {
        byte[] ConvertHtmlToPdf(string htmlContent);
    }
}
