using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Console.WriteLine("___Adding New Movie___");

            /// Data

            Console.WriteLine("Enter Title for this movie:");
            var titleMovie = Console.ReadLine();

            Console.WriteLine("Enter Free Seats for this movie");
            var NumberOfFreeSeatsMovie = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Genre for this movie");
            var genreMovie = Console.ReadLine();

           // _movies.Add(new Movie(Guid.NewGuid(), titleMovie, NumberOfFreeSeatsMovie, genreMovie));

            Console.WriteLine("To return to the menu press BACKSPACE");
        }
    }
}
