using System;

using MovieTicketBooking.Infrastructure.Repositories;

namespace MovieTicketBooking.Application.Scenarious
{
    class AddNewMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;

        public AddNewMovieScenario(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
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
            var ratingMovie = float.Parse(Console.ReadLine());

            _movieRepository.AddNewMovie(Guid.NewGuid(), titleMovie, NumberOfFreeSeatsMovie, genreMovie, ratingMovie);

            Console.WriteLine("To return to the menu press BACKSPACE");
        }
    }
}
