using BlazorApp.Shared;
using BlazorApp.Shared.ConfigurationValues;
using BlazorApp.Shared.Managers;
using Core.Framework.Extensions;
using System.IO;
using System.Reactive.Linq;

namespace Core.Framework.Domain
{
    public class PdfManager : IPdfManager
    {
        private readonly IFileManager _fileManager;
        private readonly AwsSettings _awsSettings;

        public PdfManager(IFileManager fileManager, AwsSettings awsSettings)
        {
            _fileManager = fileManager;
            _awsSettings = awsSettings;
            _fileManager.Root = _awsSettings.DocumentsBucketName;
        }

        public string GeneratePdf(PdfDocument pdf, string filename)
            => Observable.Defer(() =>
            {
                _fileManager.Save(new MemoryStream(pdf.GeneratePdf()), filename);
                return Observable.Return(filename.SetCDN());
            })
            .Catch(Observable.Return((string)null))
            .Wait();

        public string SavePdf(byte[] pdf, string filename)
            => Observable.Defer(() =>
            {
                _fileManager.Save(new MemoryStream(pdf), filename);
                return Observable.Return(filename.SetCDN());
            })
            .Catch(Observable.Return((string)null))
            .Wait();
    }
}
