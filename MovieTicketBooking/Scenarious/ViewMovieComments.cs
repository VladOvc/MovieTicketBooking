using MovieTicketBooking.Repositories;
using System;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    class ViewMovieComments : IRunnable
    {
        private MovieRepository _movieRepository;

        public ViewMovieComments(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Run()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter movie order in list");

                var movieNumber = int.Parse(Console.ReadLine());

                var selectedMovie = _movieRepository.GetMovieByIndex(movieNumber - 1);

                Console.Clear();

                foreach (var item in selectedMovie.Comments)
                {
                    Console.WriteLine();

                    Console.WriteLine($"Name: {item.User}");
                    Console.WriteLine($"Text: {item.Message}");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("!!!You entered the wrong number!!!");
            }

            Console.WriteLine();
            Console.WriteLine("To return to the menu press BACKSPACE");
        }
    }
}
