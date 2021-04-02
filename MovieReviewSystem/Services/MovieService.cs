using MovieReviewSystem.Interfaces;
using MovieReviewSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewSystem.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieReviewDBContext _dbContext;
        public MovieService(MovieReviewDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        //public MovieViewModel GetMovieByGenre(int genreId = 0)
        //{

        //}
        public List<Genre> GetAllGenre()
        {
            List<Genre> genres = _dbContext.Genres.ToList();
            return genres;
        }
        //public MovieViewModel CreateMovie(MovieViewModel model)
        //{

        //}
    }
}
