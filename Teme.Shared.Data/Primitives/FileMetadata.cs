using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Theme.Shared.Data.Primitives
{
    public class FileMetadata
    {
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string ParentId { get; set; }
        public int Version { get; set; }

        public FileMetadata(BsonDocument bsonDoc = null)
        {
            if (bsonDoc == null) return;
            EntityType = bsonDoc["EntityType"].AsString;
            EntityId = bsonDoc["EntityId"].AsString;
            ParentId = bsonDoc["ParentId"].AsString;
            Version = bsonDoc["Version"].AsInt32;
        }

        public BsonDocument ToBsonDoc()
        {
            return new BsonDocument
            {
                { "EntityType", EntityType },
                { "EntityIi", EntityId },
                { "ParentId", ParentId },
                { "Version", Version }
            };
        }
    }


}
