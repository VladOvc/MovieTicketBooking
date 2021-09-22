using MovieTicketBooking.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Scenarious
{
    class SearchMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;

        private List<BookedTicket>_bookings;
        private string _pathBookedTickets;

        public SearchMovieScenario(MovieRepository movieRepository, List<BookedTicket> bookings, string pathBookedTickets)
        {
            _movieRepository = movieRepository;
            _bookings = bookings;
            _pathBookedTickets = pathBookedTickets;
        }

        public void Run()
        {

            try
            {
                Console.Clear();

                Console.WriteLine("Enter movie title or genre or rating:");
                var titleToSearch = Console.ReadLine();
                var specifier = "0.0";

                Movie foundMovie = _movieRepository.FindMovie(titleToSearch, specifier);

                Console.WriteLine();

                Console.WriteLine($"We found movie: {foundMovie.Title}");
                Console.WriteLine($"Free seats this movie: {foundMovie.NumberOfFreeSeats}");

                Console.WriteLine();

                RenderMenuForFoundMovie();

                ConsoleKeyInfo KeyInfo = Console.ReadKey();

                switch (KeyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new BookSpecificMovieScenario(_movieRepository, _bookings, _pathBookedTickets, foundMovie).Run();
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
                        new AddNewMovie(_movies, _pathToMoviesFile).Run();
                        break;
                }
            }
        }

        private void RenderMenuForFoundMovie()
        {
            Console.WriteLine("1: Book this Movie");
            Console.WriteLine("2: View all reservations for this movie");
            Console.WriteLine("3: View Comments on this movie");
            Console.WriteLine("To return to the menu press BACKSPACE");
        }
    }
}
