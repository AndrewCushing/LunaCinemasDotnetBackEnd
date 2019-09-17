using System;
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

        public override bool Equals(object obj)
        {
            try
            {
                Film filmToCompareTo = (Film) obj;
                return IsReleased == filmToCompareTo.IsReleased &&
                       Title.Equals(filmToCompareTo.Title) &&
                       Id.Equals(filmToCompareTo.Id) &&
                       Length.Equals(filmToCompareTo.Length) &&
                       BriefDescription.Equals(filmToCompareTo.BriefDescription) &&
                       DetailedDescription.Equals(filmToCompareTo.DetailedDescription) &&
                       ImagePath.Equals(filmToCompareTo.ImagePath) &&
                       Year.Equals(filmToCompareTo.Year) &&
                       Classification.Equals(filmToCompareTo.Classification) &&
                       compareStringArr(Directors, filmToCompareTo.Directors) &&
                       compareStringArr(Actors, filmToCompareTo.Actors) &&
                       compareStringArr(Genres, filmToCompareTo.Genres);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool compareStringArr(string[] arr1, string[] arr2)
        {
            bool match = arr1.Length == arr2.Length;
            if (match)
            {
                for (int i = 0 ; i < arr2.Length ; i++)
                {
                    match = match && arr1[i].Equals(arr2[i]);
                }
            }
            return match;
        }
    }
}