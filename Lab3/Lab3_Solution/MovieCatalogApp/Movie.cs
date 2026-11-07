using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MovieCatalogApp
{
    public class Movie { 

        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [JsonPropertyOrder(2)]
        [JsonPropertyName("movie_title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyOrder(3)]
        public string Genre { get; set; } = string.Empty;

        [JsonPropertyOrder(4)]
        public int ReleaseYear { get; set; }


        [JsonPropertyOrder(5)]
        public List<string> MainActors { get; set; } = new();

        [JsonIgnore]
        public int Age => DateTime.Now.Year - ReleaseYear;
        [JsonIgnore]
        public string ActorsDisplay =>
            MainActors != null && MainActors.Count > 0
                ? string.Join(", ", MainActors)
                : "Актори не вказані";
    }
}
