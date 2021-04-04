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
    public class ReviewController : ControllerBase
    {
        public readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [Route("getReviewsByMovieId/{movieId}")]
        public IActionResult GetReviewsByMovieId(int movieId)
        {
            var result = _reviewService.getReviewsByMovieId(movieId);

            if (result != null && result.Any()) {
                return Ok(result);
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Internal Server Error." });
            }
        }

        [HttpPost]
        [Route("saveReview")]
        public IActionResult SaveReview(Review model)
        {
            var result = _reviewService.SaveReview(model);

            if (result != null) {
                return Ok(result);
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Internal Server Error." });
            }
        }

        [HttpGet]
        [Route("deleteReview/{movieId}")]
        public IActionResult DeleteReview(int movieId)
        {
            var result = _reviewService.DeleteReview(movieId);

            if (result) {
                return Ok(result);
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Internal Server Error." });
            }
        }
    }
}
