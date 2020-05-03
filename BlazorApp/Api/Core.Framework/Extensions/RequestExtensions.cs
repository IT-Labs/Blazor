using Core.Shared;
using Core.Shared.Requests;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Core.Framework.Extensions
{
    public static class RequestExtensions
    {
        public static SetActiveStatusRequest<T> ToActiveStatusRequest<T>(this IdRequest request, bool isActive = false)
            where T : AuditableEntity
        {
            return new SetActiveStatusRequest<T>(request, isActive);
        }

        public static string GetDocumentContents(this HttpRequest Request)
        {
            string documentContents;
            using (Stream receiveStream = Request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            return documentContents;
        }
    }
}