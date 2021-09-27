using System;

using MovieTicketBooking.Infrastructure.Repositories;

namespace MovieTicketBooking.Application.Scenarious
{
    class SortMoviesScenario : IRunnable
    {
        private MovieRepository _movieRepository;

        public SortMoviesScenario(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Run()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("by what criterion to sort movies?");

            Console.WriteLine("1 Sort by Title");
            Console.WriteLine("2 Sort by Genre");
            Console.WriteLine("3 Sort by Rating");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    ///_movieRepository.SortMovieTitle();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    ///_movieRepository.SortMovieGenre();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    ///_movieRepository.SortMovieRating();
                    break;
            }
        }
    }
}
