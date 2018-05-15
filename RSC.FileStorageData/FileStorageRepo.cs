using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Theme.Shared.Data.Primitives;

namespace RSC.FileStorageData
{
    public class FileStorageRepo : BaseRepo, IFileStorageRepo
    {
        private readonly IGridFSBucket _bucket;

        public FileStorageRepo(IConfiguration config) : base(config)
        {
            _bucket = new GridFSBucket(GetDatabase());
        }

        public async Task<string> UploadFromStream(string filename, Stream stream, FileMetadata metadata)
        {
            var id = await _bucket.UploadFromStreamAsync(filename, stream, new GridFSUploadOptions
            {
                Metadata = metadata.ToBsonDoc()
            });
            return id.ToString();
        }

        public async Task<Stream> DownloadFromStream(string fileId, bool seekable = false)
        {
            var res = await _bucket.OpenDownloadStreamAsync(ObjectId.Parse(fileId), new GridFSDownloadOptions { Seekable = seekable });
            return res;
        }

        public async Task<FileMetadata> GetFile(string entityType, string entityId)
        {
            var filterBuilder = Builders<GridFSFileInfo>.Filter;
            var filter = filterBuilder.Eq(x => x.Metadata["EntityType"], entityType)
                | filterBuilder.Eq(x => x.Metadata["EntityId"], entityId);
            var res = await _bucket.Find(filter).ToListAsync();
            var meta = res.FirstOrDefault()?.Metadata;
            return new FileMetadata(meta);
        }
    }
}
