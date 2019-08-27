using LunaCinemasBackEndInDotNet.BusinessLogic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Film
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("released")]
        public bool IsReleased { get; set; }
        [BsonElement("length")]
        public string Length { get; set; }
        [BsonElement("briefDescription")]
        public string BriefDescription { get; set; }
        [BsonElement("detailedDescription")]
        public string DetailedDescription { get; set; }
        [BsonElement("imagePath")]
        public string ImagePath { get; set; }
        [BsonElement("year")]
        public string Year { get; set; }
        [BsonElement("classification")]
        public string Classification { get; set; }
        [BsonElement("directors")]
        public string[] Directors { get; set; }
        [BsonElement("genres")]
        public string[] Genres { get; set; }
        [BsonElement("actors")]
        public string[] Actors { get; set; }
        [BsonElement("_class")]
        public string JavaClass { get; set; }
    }
}