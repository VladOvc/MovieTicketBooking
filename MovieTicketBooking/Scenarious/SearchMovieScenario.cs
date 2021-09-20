using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Scenarious
{
    class SearchMovieScenario : IRunnable
    {
        private List<Movie> _movies;

        public SearchMovieScenario(List<Movie> movies)
        {
            _movies = movies;
        }

        public void Run()
        {

            try
            {
                Console.Clear();

                Console.WriteLine("Enter movie title:");
                var titleToSearch = Console.ReadLine();

                var foundMovie = _movies.Where(item => item.Title.ToLower().Contains(titleToSearch.ToLower())).First();

                Console.WriteLine($"We found movie: {foundMovie.Title}");
                Console.WriteLine($"Free seats this movie: {foundMovie.NumberOfFreeSeats}");
                Console.WriteLine("To return to the menu press BACKSPACE");

                ConsoleKeyInfo KeyInfo = Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine();
                Console.WriteLine("Movie not a found");
            }
        }
    }
}
