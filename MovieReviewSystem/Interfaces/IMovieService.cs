using MovieReviewSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewSystem.Interfaces
{
    public interface IMovieService
    {
        //public MovieViewModel GetMovieByGenre(int genreId = 0);
        public List<Genre> GetAllGenre();
        //public MovieViewModel CreateMovie(MovieViewModel model);

    }
}
