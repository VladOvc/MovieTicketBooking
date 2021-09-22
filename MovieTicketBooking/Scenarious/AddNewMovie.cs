using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MovieTicketBooking.Scenarious
{
    class AddNewMovie : IRunnable
    {
        private List<Movie> _movies;
        private string _pathToMoviesFile;

        public AddNewMovie(List<Movie> movies, string pathToMoviesFile)
        {
            _movies = movies;
            _pathToMoviesFile = pathToMoviesFile;
        }

        public void Run()
        {
            Console.WriteLine();
            Console.WriteLine("___Adding New Movie___");

            /// Data

            Console.WriteLine("Enter Title:");
            var titleMovie = Console.ReadLine();

            Console.WriteLine("Enter Free Seats:");
            var NumberOfFreeSeatsMovie = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Genre:");
            var genreMovie = Console.ReadLine();

            Console.WriteLine("Enter Rating:");
            var ratingMovie = Console.ReadLine();

            _movies.Add(new Movie(Guid.NewGuid(), titleMovie, NumberOfFreeSeatsMovie, genreMovie, ratingMovie));

            File.WriteAllText(_pathToMoviesFile, JsonConvert.SerializeObject(_movies, Formatting.Indented));

            Console.WriteLine("To return to the menu press BACKSPACE");
        }
    }
}
