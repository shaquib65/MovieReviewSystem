using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReviewSystem.Authentication;
using MovieReviewSystem.Interfaces;
using MovieReviewSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("getAllGenre")]
        public IActionResult GetAllGenre()
        {
            var result = _movieService.GetAllGenre();

            if (result != null && result.Any()) {
                return Ok(result);
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Internal Server Error." });
            }
        }
        [HttpPost]
        [Route("createMovie")]
        public IActionResult CreateMovie(MovieViewModel model)
        {
            var result = _movieService.CreateMovie(model);

            if (result != null) {
                return Ok(result);
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Internal Server Error." });
            }
        }

        [HttpGet]
        [Route("getMoviesByGenre/{genreId}")]
        public IActionResult GetMoviesByGenre(int genreId) 
        {
            var result = _movieService.GetMoviesByGenre(genreId);

            if (result != null) {
                return Ok(result);
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Internal Server Error." });
            }
        }

        [HttpGet]
        [Route("getMoviesByMovieId/{movieId}")]
        public IActionResult GetMoviesByMovieId(int movieId)
        {
            var result = _movieService.GetMoviesByMovieId(movieId);

            if (result != null) {
                return Ok(result);
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Internal Server Error." });
            }
        }

        [HttpPost]
        [Route("updateMovie")]
        public IActionResult UpdateMovie(MovieViewModel model)
        {
            var result = _movieService.UpdateMovie(model);

            if (result != null) {
                return Ok(result);
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Internal Server Error." });
            }
        }

        [HttpGet]
        [Route("deleteMovie/{movieId}")]
        public IActionResult DeleteMovie(int movieId)
        {
            var result = _movieService.DeleteMovie(movieId);

            if (result) {
                return Ok(result);
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Internal Server Error." });
            }
        }
    }
}
