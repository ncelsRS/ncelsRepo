using System.IO;
using System.Threading.Tasks;
using Theme.Shared.Data.Primitives;

namespace RSC.FileStorageLogic
{
    public interface IFileLogic
    {
        Task<string> UploadFromStream(string filename, Stream stream, FileMetadata meta);
    }
}