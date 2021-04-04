using MovieReviewSystem.Interfaces;
using MovieReviewSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewSystem.Services
{
    public class ReviewService:IReviewService
    {
        private readonly MovieReviewDBContext _dbContext;
        public ReviewService(MovieReviewDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public List<Review> getReviewsByMovieId(int movieId)
        {
            List<Review> reviews = _dbContext.Reviews.Where(x => x.MovieId == movieId).ToList();
            if(reviews != null && reviews.Any())
            {
                return reviews;
            }
            else
            {
                return null;
            }
        }
        public Review SaveReview(Review model)
        {
            if(model != null && model.ReviewId == 0)
            {
                _dbContext.Reviews.Add(model);
            }
            else
            {
                _dbContext.Reviews.Update(model);
            }
            _dbContext.SaveChanges();
            return model;
        }
        public bool DeleteReview(int reviewId)
        {
            Review review = _dbContext.Reviews.Where(x => x.ReviewId == reviewId).FirstOrDefault();

            if(review  != null)
            {
                _dbContext.Reviews.Remove(review);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
