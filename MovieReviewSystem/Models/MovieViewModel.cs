using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewSystem.Models
{
    public class MovieViewModel
    {
        [Key]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public string CastCrew { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Budget { get; set; }
        public string Language { get; set; }
        public string CoverImage { get; set; }
        public string TrailerUrl { get; set; }
        public double OverallRating { get; set; }
        public Review CurrentReview { get; set; }
        public List<int> Genres { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
