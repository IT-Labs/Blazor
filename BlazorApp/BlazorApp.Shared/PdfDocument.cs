namespace BlazorApp.Shared
{
    public abstract class PdfDocument
    {
        public abstract byte[] GeneratePdf();
    }
}
