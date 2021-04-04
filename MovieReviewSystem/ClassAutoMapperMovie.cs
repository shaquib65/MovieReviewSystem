using AutoMapper;
using MovieReviewSystem.Models;

namespace MovieReviewSystem
{
    public class ClassAutoMapperMovie : Profile
    {
        public ClassAutoMapperMovie()
        {
            CreateMap<Movie, MovieViewModel>();
            CreateMap<MovieViewModel, Movie>();
        }
    }
}
