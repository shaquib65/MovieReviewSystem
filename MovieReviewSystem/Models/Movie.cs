using System;
using System.Collections.Generic;

#nullable disable

namespace MovieReviewSystem.Models
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public string CastCrew { get; set; }
        public string TrailerUrl { get; set; }

        public DateTime ReleaseDate { get; set; }
        public int Budget { get; set; }
        public string Language { get; set; }
        public string CoverImage { get; set; }
    }
}
