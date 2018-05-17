using RSC.FileStorageData.Dtos;
using RSC.FileStorageLogic.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Theme.Shared.Data.Primitives;

namespace RSC.FileStorageData
{
    public interface IFileStorageRepo
    {
        Task<Stream> DownloadFromStream(string fileId, bool seekable = false);
        Task<IEnumerable<FileInfoFullDto>> GetFiles(string entityType, string entityId, string fileType);
        Task<FileInfoDto> GetFileInfo(string fileId);
        Task<string> UploadFromStream(string filename, Stream stream, FileMetadata metadata = null);
    }
}
