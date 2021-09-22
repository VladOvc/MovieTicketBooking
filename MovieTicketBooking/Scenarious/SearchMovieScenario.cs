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
        private List<BookedTicket>_bookings;
        private string _pathToMoviesFile;
        private string _pathBookedTickets;

        public SearchMovieScenario(List<Movie> movies, List<BookedTicket> bookings, string pathToMoviesFile, string pathBookedTickets)
        {
            _movies = movies;
            _bookings = bookings;
            _pathToMoviesFile = pathToMoviesFile;
            _pathBookedTickets = pathBookedTickets;
        }

        public void Run()
        {

            try
            {
                Console.Clear();

                Console.WriteLine("Enter movie title:");
                var titleToSearch = Console.ReadLine();
                var spec = "0.0";
                var foundMovie = _movies.Where(item => item.Title.ToLower().Contains(titleToSearch.ToLower())
                                                    || item.Genre.ToLower().Contains(titleToSearch.ToLower())
                                                    || item.Rating.ToString(spec) == titleToSearch)
                                                    .First();

                Console.WriteLine($"We found movie: {foundMovie.Title}");
                Console.WriteLine($"Free seats this movie: {foundMovie.NumberOfFreeSeats}");

                RenderMainMenu();

                ConsoleKeyInfo KeyInfo = Console.ReadKey();

                switch (KeyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new BookMovieScenario(_movies, _bookings, _pathToMoviesFile, _pathBookedTickets, foundMovie).Run();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        new ViewMovieReservation(_bookings, foundMovie).Run();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        new ViewMovieComments(_movies, foundMovie).Run();
                        break;
                    default:
                        break;
                }

                Console.WriteLine("To return to the menu press BACKSPACE");

            }
            catch (InvalidOperationException)
            {
                Console.WriteLine();
                Console.WriteLine("Movie not a found");

                ConsoleKeyInfo KeyInfo = Console.ReadKey();

                switch (KeyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new AddNewMovie(_movies, _pathToMoviesFile);
                        break;
                }
            }
        }

        private void RenderMainMenu()
        {
            Console.WriteLine("1: Book this Movie");
            Console.WriteLine("2: View all reservations for this movie");
            Console.WriteLine("3: View Comments on this movie");
            Console.WriteLine("Backspace: Ввернуться в главное меню");
        }
    }
}
