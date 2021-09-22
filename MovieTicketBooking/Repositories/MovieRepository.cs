using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Repositories
{
    public class MovieRepository
    {
        private List<Movie> _movies;

        private string _pathToMovies = "../../../Files/Movies.json";

        public MovieRepository(List<Movie> movies)
        {

            _movies = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(_pathToMovies));
        }
        public List<Movie> GetAll()
        {


            return _movies;
        }

        public Movie FindMovie(string titleToSearch, string specifier)
        {
            var foundMovie = _movies.Where(item => item.Title.ToLower().Contains(titleToSearch.ToLower())
                                                    || item.Genre.ToLower().Contains(titleToSearch.ToLower())
                                                    || item.Rating.ToString(specifier) == titleToSearch)
                                                    .First();
            return foundMovie;
        }

        public void Save()
        {
            File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));
        }
    }
}
