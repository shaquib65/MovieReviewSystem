using AutoMapper;
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
        private readonly IMapper _mapper;
        public MovieService(MovieReviewDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }
        public List<MovieViewModel> GetMoviesByGenre(int genreId)
        {
            List<Movie> movies = new List<Movie>();
            List<MovieGenreMapping> movieGenreMappings = new List<MovieGenreMapping>();
            List<Review> reviews = new List<Review>();
            List<MovieViewModel> returnObject = new List<MovieViewModel>();
            if (genreId == 0)
            {
                movies = _dbContext.Movies.ToList();
                reviews = _dbContext.Reviews.ToList();
                movieGenreMappings = _dbContext.MovieGenreMappings.ToList();
            }
            else
            {
                var genres = _dbContext.MovieGenreMappings.Where(x => x.GenreId == genreId).ToList();
                movies = _dbContext.Movies.Where(x => genres.Select(x => x.MovieId).Contains(x.MovieId)).ToList();
                reviews = _dbContext.Reviews.Where(x => movies.Select(x => x.MovieId).Contains(x.MovieId)).ToList();
            }
            foreach (var movie in movies)
            {
                returnObject.Add(new MovieViewModel
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    CoverImage = movie.CoverImage,
                    OverallRating = reviews.Where(x => x.MovieId == movie.MovieId).Select(x => x.Rating).Average(),
                    Genres = movieGenreMappings.Where(x => x.MovieId == movie.MovieId).Select(x => x.GenreId).ToList()
                }); ;
            }
            return returnObject;
        }
        public List<Genre> GetAllGenre()
        {
            List<Genre> genres = _dbContext.Genres.ToList();
            return genres;
        }
        public MovieViewModel CreateMovie(MovieViewModel model)
        {
            List<MovieGenreMapping> saveGenre = new List<MovieGenreMapping>();
            List<Review> savedReview = new List<Review>();
            Movie movie = _mapper.Map<Movie>(model);

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            var movieId = movie.MovieId;
            if (model.Genres.Any())
            {
                foreach (var genre in model.Genres)
                {
                    saveGenre.Add(new MovieGenreMapping { GenreId = genre, MovieId = movieId });
                }
                _dbContext.MovieGenreMappings.AddRange(saveGenre);

            }
            if (model.Reviews.Any())
            {
                foreach (var review in model.Reviews)
                {
                    review.MovieId = movieId;
                    savedReview.Add(review);
                }
                _dbContext.Reviews.AddRange(savedReview);
            }

            _dbContext.SaveChanges();
            MovieViewModel movieModel = _mapper.Map<MovieViewModel>(movie);
            if (saveGenre.Any())
            {
                movieModel.Genres = saveGenre.Select(x => x.GenreId).ToList();
            }
            if (savedReview.Any())
            {
                movieModel.Reviews = savedReview;
            }
            return movieModel;
        }

        public MovieViewModel UpdateMovie(MovieViewModel model)
        {
            List<MovieGenreMapping> saveGenre = new List<MovieGenreMapping>();
            List<MovieGenreMapping> deleteGenre = new List<MovieGenreMapping>();

            Movie movie = _mapper.Map<Movie>(model);
            _dbContext.Movies.Update(movie);
            if (model.Genres.Any())
            {
                deleteGenre = _dbContext.MovieGenreMappings.Where(x => x.MovieId == model.MovieId).ToList();
                _dbContext.MovieGenreMappings.RemoveRange(deleteGenre);
                foreach (var genre in model.Genres)
                {
                    saveGenre.Add(new MovieGenreMapping { GenreId = genre, MovieId = movie.MovieId });
                }
                _dbContext.MovieGenreMappings.AddRange(saveGenre);

            }
            _dbContext.SaveChanges();
            MovieViewModel movieModel = _mapper.Map<MovieViewModel>(movie);
            if (saveGenre.Any())
            {
                movieModel.Genres = saveGenre.Select(x => x.GenreId).ToList();
            }

            return movieModel;
        }

        public bool DeleteMovie(int movieId)
        {
            Movie movie = _dbContext.Movies.FirstOrDefault(x => x.MovieId == movieId);
            if (movie != null && movie.MovieId != 0)
            {
                List<MovieGenreMapping> movieGenreMappings = _dbContext.MovieGenreMappings.Where(x => x.MovieId == movieId).ToList();
                List<Review> reviews = _dbContext.Reviews.Where(x => x.MovieId == movieId).ToList();
                if (movieGenreMappings.Any())
                {
                    _dbContext.MovieGenreMappings.RemoveRange(movieGenreMappings);
                }
                if (reviews.Any())
                {
                    _dbContext.Reviews.RemoveRange(reviews);
                }
                _dbContext.Movies.Remove(movie);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public MovieViewModel GetMoviesByMovieId(int movieId)
        {
            Movie movie = _dbContext.Movies.FirstOrDefault(x => x.MovieId == movieId);
            List<int> movieGenreMappings = _dbContext.MovieGenreMappings.Where(x => x.MovieId == movieId).Select(x => x.GenreId).ToList();
            List<Review> reviews = _dbContext.Reviews.Where(x => x.MovieId == movieId).ToList();
            if (movie != null && movie.MovieId != 0)
            {
                MovieViewModel returnObject = new MovieViewModel
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    Plot = movie.Plot,
                    CastCrew = movie.CastCrew,
                    ReleaseDate = movie.ReleaseDate,
                    Budget = movie.Budget,
                    Language = movie.Language,
                    CoverImage = movie.CoverImage,
                    TrailerUrl = movie.CoverImage,
                    OverallRating = (long)reviews.Select(x => x.Rating).Average(),
                    Genres = movieGenreMappings,
                    Reviews = reviews
                };
                return returnObject;
            }
            else
            {
                return null;
            }

        }
    }
}
