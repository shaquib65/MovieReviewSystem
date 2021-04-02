using System;
using System.Collections.Generic;

#nullable disable

namespace MovieReviewSystem.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int MovieId { get; set; }
    }
}
