using System;
using System.Collections.Generic;

#nullable disable

namespace MovieReviewSystem.Models
{
    public partial class MovieGenreMapping
    {
        public int GenreId { get; set; }
        public int MovieId { get; set; }
    }
}
