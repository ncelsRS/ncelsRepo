using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Theme.Shared.Data.Primitives
{
    public class FileMetadata
    {
        public string Id { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string ParentId { get; set; }
        public int Version { get; set; }
        public string FileType { get; set; }
        public string ContentType { get; set; }
    }

}
