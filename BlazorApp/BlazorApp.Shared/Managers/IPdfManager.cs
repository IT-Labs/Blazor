namespace BlazorApp.Shared.Managers
{
    public interface IPdfManager
    {
        string GeneratePdf(PdfDocument pdf, string filename);
        string SavePdf(byte[] pdf, string filename);
    }
}
