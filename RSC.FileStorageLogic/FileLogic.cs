using RSC.FileStorageData;
using System.IO;
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

        public async Task<string> UploadFromStream(string filename, Stream stream, FileMetadata meta)
        {
            var id = await _repo.UploadFromStream(filename, stream, meta);
            return id;
        }
    }
}
