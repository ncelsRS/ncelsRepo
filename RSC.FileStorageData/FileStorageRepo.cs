using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using RSC.FileStorageData.Dtos;
using RSC.FileStorageLogic.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Theme.Shared.Data.Primitives;

namespace RSC.FileStorageData
{
    public class FileStorageRepo : BaseRepo, IFileStorageRepo
    {
        private readonly IGridFSBucket _bucket;
        private readonly IMongoCollection<GridFSFileInfo<ObjectId>> _col;

        public FileStorageRepo(IConfiguration config) : base(config)
        {
            var db = GetDatabase();
            _bucket = new GridFSBucket(db);
            _col = db.GetCollection<GridFSFileInfo<ObjectId>>("fs.files");
        }

        public async Task<string> UploadFromStream(string filename, Stream stream, FileMetadata metadata = null)
        {
            var options = metadata == null ? null : new GridFSUploadOptions
            {
                Metadata = metadata.ToBsonDocument()
            };
            var id = await _bucket.UploadFromStreamAsync(filename, stream, options);
            return id.ToString();
        }

        public async Task<Stream> DownloadFromStream(string fileId, bool seekable = false)
        {
            var res = await _bucket.OpenDownloadStreamAsync(ObjectId.Parse(fileId), new GridFSDownloadOptions { Seekable = seekable });
            return res;
        }

        public async Task<FileInfoDto> GetFileInfo(string fileId)
        {
            return await _col.Find(x => x.Id == ObjectId.Parse(fileId))
                .Project(x => new FileInfoDto
                {
                    ContentType = x.Metadata.GetValue("ContentType").AsString,
                    FileName = x.Filename
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FileInfoFullDto>> GetFiles(string entityType, string entityId, string fileType)
        {
            var res = await _col
                .Find(x => x.Metadata["EntityType"] == entityType
                    && (x.Metadata["EntityId"] == entityId || entityId == "null")
                    && x.Metadata["FileType"] == fileType)
                .Project(x => new
                {
                    x.Id,
                    x.Filename
                })
                .ToListAsync();
            return res.Select(x => new FileInfoFullDto { Id = x.Id.ToString(), Filename = x.Filename });
        }
    }
}
