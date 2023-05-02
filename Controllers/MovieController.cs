using Microsoft.AspNetCore.Mvc;
using MovieApi_2023.Models;
using System.Numerics;
using System.Xml.Linq;

namespace MovieApi_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        MovieDbContext movieDb = new MovieDbContext();
        Random rnd = new Random();


        [HttpGet("AllMovies")]
        public ActionResult<List<Movie>> GetAllMovies()
        {
            return movieDb.Movies.OrderBy(x => x.Id).ToList();
        }



        [HttpGet("MoviesOfCategory")]
        public IEnumerable<Movie> GetMoviesOfCategory(string category)
        {
            var result = movieDb.Movies.Where(x => x.Category == category);
            if (result != null)
            {
                return result;
            }
            else
            {
                return Enumerable.Empty<Movie>();
            }
        }
        [HttpGet("RandomMovie")]
        public List<Movie> GetRandomMovie()
        {
            var count = movieDb.Movies.Count();
            var index = rnd.Next(count);
            var randomMovie = movieDb.Movies.Skip(index).Take(1);
            return randomMovie.ToList();
        }

        [HttpGet("RandomMoviePick")]
        public IEnumerable<Movie> GetRandomMovieOfCategory(string movieCategory)
        {
            var count = movieDb.Movies.Count(x => x.Category == movieCategory);
            if (count == 0)
            {
                return Enumerable.Empty<Movie>();
            }
            var index = rnd.Next(count);
            var randomMovie = movieDb.Movies.Where(x => x.Category == movieCategory)
                                             .Skip(index).Take(1);
            return randomMovie;
        }

        [HttpGet("RandomMovieList")]
        public ActionResult<IEnumerable<Movie>> GetRandomMovieList(int noOfMovies)
        {
            var movieCount = movieDb.Movies.Count();
            if (noOfMovies >= movieCount)
            {
                return NotFound($"We only have {movieCount} movies");
            }

            var index = rnd.Next(movieCount - noOfMovies + 1);
            var randomMovieList = movieDb.Movies.OrderBy(x => x.Id)
                                                .Skip(index)
                                                .Take(noOfMovies)
                                                .ToList();
            return randomMovieList;
        }



        [HttpGet("AllCategories")]
        public IEnumerable<string> GetAllMovieCategories()
        {
            var categoriesList = movieDb.Movies.Select(x => x.Category).Distinct();
            return categoriesList;
        }
        [HttpGet("SpecificMovie")]
        public IEnumerable<Movie> GetSpecificMovie(string thisMovie)
        {
            var myMovie = movieDb.Movies.Where(x => x.Title == thisMovie);
            if (myMovie == null)
            {
                return Enumerable.Empty<Movie>();
            }
            return myMovie;
        }

        [HttpGet("MoviesWithName")]
        public IEnumerable<Movie> GetAllMoviesWithName(string movieName)
        {
            List<Movie> myMovies = new List<Movie>();
            foreach (var currMovie in movieDb.Movies)
            {
                if (currMovie.Title.Contains(movieName, StringComparison.CurrentCultureIgnoreCase))
                {
                    myMovies.Add(currMovie);
                }
            }
            return myMovies;
        }
    }
}