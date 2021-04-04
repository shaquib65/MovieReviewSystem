using System;
using System.Collections.Generic;

#nullable disable

namespace MovieReviewSystem.Models
{
    public partial class MovieGenreMapping
    {
        public int MovieGenreMappingId { get; set; }
        public int GenreId { get; set; }
        public int MovieId { get; set; }
    }
}
