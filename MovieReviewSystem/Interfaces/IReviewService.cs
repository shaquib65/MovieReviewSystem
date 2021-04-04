using MovieReviewSystem.Models;
using System.Collections.Generic;

namespace MovieReviewSystem.Interfaces
{
    public interface IReviewService
    {
        public List<Review> getReviewsByMovieId(int movieId);
        public Review SaveReview(Review model);
        public bool DeleteReview(int reviewId);
    }
}
