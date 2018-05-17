using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RSC.FileStorageData;
using RSC.FileStorageData.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Theme.Shared.Data.Primitives;

namespace RSC.FileStorageLogic
{
    public class FileLogic : IFileLogic
    {
        private readonly IFileStorageRepo _repo;
        public FileLogic(IFileStorageRepo repo)
        {
            _repo = repo;
        }

        private string GetBoundary(string contentType)
        {
            var elements = contentType.Split(' ');
            var element = elements.FirstOrDefault(entry => entry.StartsWith("boundary="));
            var boundary = element.Substring("boundary=".Length);
            // Remove quotes
            if (boundary.Length >= 2 && boundary[0] == '"' &&
                boundary[boundary.Length - 1] == '"')
            {
                boundary = boundary.Substring(1, boundary.Length - 2);
            }
            return boundary;
        }

        public async Task<object> UploadFromStream(HttpContext httpContext)
        {
            string fileId = null;
            FileMetadata meta = null;
            var boundary = GetBoundary(httpContext.Request.ContentType);
            var reader = new MultipartReader(boundary, httpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue
                    .TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (contentDisposition.Name.Trim('"') != "meta")
                    {
                        meta.ContentType = section.ContentType;
                        fileId = await _repo.UploadFromStream(contentDisposition.FileName.Trim('"'), section.Body, meta);
                    }
                    else
                    {
                        string json;
                        using (var sr = new StreamReader(section.Body))
                        {
                            json = await sr.ReadToEndAsync();
                        }
                        meta = JsonConvert.DeserializeObject<FileMetadata>(json);
                    }
                }

                section = await reader.ReadNextSectionAsync();
            }

            if (fileId == null) throw new ArgumentException("File not uploaded");

            return new { fileId };
        }

        public async Task<Tuple<Stream, string, string>> DownloadAsStream(string fileId)
        {
            var contentTypeTask = _repo.GetFileInfo(fileId);
            var fileStreamTask = _repo.DownloadFromStream(fileId);
            var info = await contentTypeTask;
            var stream = await fileStreamTask;
            return new Tuple<Stream, string, string>(stream, info.ContentType, info.FileName);
        }

        public async Task<IEnumerable<FileInfoFullDto>> GetFiles(string entityType, string entityId, string fileType)
        {
            return await _repo.GetFiles(entityType, entityId, fileType);
        }
    }
}
