using MovieReviewSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewSystem.Interfaces
{
    public interface IMovieService
    {
        public List<MovieViewModel> GetMoviesByGenre(int genreId);
        public List<Genre> GetAllGenre();
        public MovieViewModel CreateMovie(MovieViewModel model);
        public MovieViewModel GetMoviesByMovieId(int movieId);
        public MovieViewModel UpdateMovie(MovieViewModel model);
        public bool DeleteMovie(int movieId);

    }
}
