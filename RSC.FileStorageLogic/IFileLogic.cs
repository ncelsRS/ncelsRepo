using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSC.FileStorageData.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Theme.Shared.Data.Primitives;

namespace RSC.FileStorageLogic
{
    public interface IFileLogic
    {
        Task<object> UploadFromStream(HttpContext httpContext);

        Task<Tuple<Stream, string, string>> DownloadAsStream(string fileId);
        Task<IEnumerable<FileInfoFullDto>> GetFiles(string entityType, string entityId, string fileType);
    }
}
