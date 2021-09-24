using MovieTicketBooking.Helpers;
using MovieTicketBooking.Repositories;
using MovieTicketBooking.Scenarious;
using System;

namespace MovieTicketBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookingRepository = new BookingRepository();

            var movieRepository = new MovieRepository();

            var uiHelper = new UIHelper(movieRepository);

            uiHelper.RenderMoviesTable();
            uiHelper.RenderMainMenu();

            /// Chouse menu item

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                var page = 1;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        Console.Clear();

                        uiHelper.RenderMoviesTable();
                        uiHelper.RenderMainMenu();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new SearchMovieScenario(movieRepository, bookingRepository).Run();

                        Console.Clear();
                        uiHelper.RenderMoviesTable();
                        uiHelper.RenderMainMenu();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        new SortMoviesScenario(movieRepository).Run();

                        Console.Clear();
                        uiHelper.RenderMoviesTable();
                        uiHelper.RenderMainMenu();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        new BookMovieScenario(movieRepository, bookingRepository).Run();
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        new CancelBooking(movieRepository, bookingRepository).Run();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        ///new ViewMovieBookings(movieRepository, bookingRepository).Run();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        new ViewMovieComments(movieRepository).Run();
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();

                        page++;
                        uiHelper.RenderMoviesTable(page);
                        uiHelper.RenderMainMenu();
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();

                        page--;
                        uiHelper.RenderMoviesTable(page);
                        uiHelper.RenderMainMenu();
                        break;

                }
            }
            while (keyInfo.Key != ConsoleKey.X);
        }
    }
}
