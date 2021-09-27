using System;

using MovieTicketBooking.Domain.Entities;
using MovieTicketBooking.Infrastructure.Repositories;

namespace MovieTicketBooking.Application.Scenarious
{
    class SearchMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;

        public SearchMovieScenario(MovieRepository movieRepository, BookingRepository bookingRepository)
        {
            _movieRepository = movieRepository;
            _bookingRepository = bookingRepository;
        }

        public void Run()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("Enter movie title or genre or rating:");
                var titleToSearch = Console.ReadLine();
                var specifier = "0.0";

                Movie foundMovie = _movieRepository.FindMovieByCriteria(titleToSearch, specifier);

                Console.WriteLine();

                Console.WriteLine($"We found movie: {foundMovie.Title}");
                Console.WriteLine($"Free seats this movie: {foundMovie.NumberOfFreeSeats}");

                Console.WriteLine();

                RenderMenuForFoundMovie();

                ConsoleKeyInfo KeyInfo;

                do
                {
                    KeyInfo = Console.ReadKey();

                    switch (KeyInfo.Key)
                    {
                        case ConsoleKey.Backspace:
                            Console.Clear();

                            Console.WriteLine($"We found movie: {foundMovie.Title}");
                            Console.WriteLine($"Free seats this movie: {foundMovie.NumberOfFreeSeats}");
                            Console.WriteLine();
                            RenderMenuForFoundMovie();
                            break;
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            new BookingSpecificMovieScenario(_movieRepository, _bookingRepository, foundMovie).Run();
                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            new ViewSpecificMovieBooking(_bookingRepository, foundMovie).Run();

                            Console.WriteLine();
                            RenderMenuForFoundMovie();
                            break;
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            new ViewSpecificMovieComments(foundMovie).Run();

                            Console.WriteLine();
                            RenderMenuForFoundMovie();
                            break;
                        default:
                            break;
                    }
                } while (KeyInfo.Key != ConsoleKey.X);

            }
            catch (InvalidOperationException)
            {
                Console.WriteLine();
                Console.WriteLine("Movie not a found");
                Console.WriteLine("1 Add new movie");
                Console.WriteLine("To return to the menu press BACKSPACE");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new AddNewMovieScenario(_movieRepository).Run();
                        break;
                    case ConsoleKey.Backspace:
                        Console.Clear();
                        break;
                }
            }
        }

        private void RenderMenuForFoundMovie()
        {
            Console.WriteLine("1: Book this Movie");
            Console.WriteLine("2: View all reservations for this movie");
            Console.WriteLine("3: View Comments on this movie");
            Console.WriteLine("Clear Console press BACKSPACE");
            Console.WriteLine("To return to the menu press X");
        }
    }
}
