using BlazorApp.Shared.ConfigurationValues;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Managers;
using BlazorApp.Shared.Requests.File;
using BlazorApp.Shared.Response;
using Core.Framework.Extensions;
using Core.Framework.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Core.Framework.Domain
{
    public class FileUploadManager : BaseManager, IFileUploadManager
    {
        private readonly IFileManager _fileManager;
        private readonly AwsSettings _awsSettings;
        private readonly IWebHostEnvironment _environment;

        public FileUploadManager(DomainRepository repository, ILogger<FileUploadManager> logger, IFileManager fileManager, AwsSettings awsSettings, IWebHostEnvironment environment) : base(repository, logger)
        {
            _fileManager = fileManager;
            _awsSettings = awsSettings;
            _environment = environment;
        }

        public Response<string> Upload(FileUploadRequest request, string fileName = null)
        {
            var response = new Response<string>();

            _fileManager.Root = SetBucketName(request.FileUploadType);

            response.Merge(ValidateBucketName(_fileManager.Root, request.FileUploadType));

            if (response.NotOk)
                return response;

            if (request.IsRequestInvalid(response, ContextInfo))
                return response;

            var newFilename = string.IsNullOrEmpty(fileName) ? request.File.FileName : fileName;

            using (var stream = request.File.OpenReadStream())
            {
                _fileManager.Save(stream, $@"\{newFilename}");
            }

            response.Payload = newFilename;

            return response;
        }

        public Response<FileDownloadResponse> Download(string path)
        {
            var response = new Response<FileDownloadResponse>();

            if (string.IsNullOrWhiteSpace(path))
                return response;
            try
            {
                string contentType;
                response.Payload = new FileDownloadResponse
                {
                    FileName = Path.GetFileName(path),
                    ContentType = new FileExtensionContentTypeProvider().TryGetContentType(path, out contentType)
                           ? contentType : "application/octet-stream",
                    Stream = _fileManager.Read(path)
                };
            }
            catch (Exception exception)
            {
                Logger.LogError(new EventId(), exception, exception.Message);
                response.Status = HttpStatusCode.InternalServerError;
                return response;
            }

            return response;
        }

        public Response<bool> Delete(FileDeleteRequest request)
        {
            var response = new Response<bool>();

            _fileManager.Root = SetBucketName(request.FileUploadType);

            response.Merge(ValidateBucketName(_fileManager.Root, request.FileUploadType));

            if (response.NotOk)
                return response;

            if (string.IsNullOrWhiteSpace(request.Path))
                return response;

            _fileManager.Delete(request.Path);
            return response;
        }

        private string SetBucketName(FileUploadType type)
        {
            return type switch
            {
                FileUploadType.Image => string.Format(_awsSettings.ImagesBucketName.ToLower(), _environment.EnvironmentName.ToLower()),
                FileUploadType.Document => string.Format(_awsSettings.DocumentsBucketName.ToLower(), _environment.EnvironmentName.ToLower()),
                _ => string.Empty,
            };
        }

        private Response<bool> ValidateBucketName(string bucketName, FileUploadType type)
        {
            if (string.IsNullOrWhiteSpace(bucketName))
                return new Response<bool>
                {
                    Payload = false,
                    Status = HttpStatusCode.BadRequest,
                    Messages = new List<ResponseMessage>
                    {
                        new ResponseMessage(ResponseMessageType.Exception)
                        {
                            Message = $"Unable to find the S3 bucket for FileUploadType:{type.ToString()}"
                        }
                    }
                };

            return new Response<bool> { Status = HttpStatusCode.OK, Payload = true };
        }
    }
}
