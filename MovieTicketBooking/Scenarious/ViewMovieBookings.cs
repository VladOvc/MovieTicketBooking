using System;

using MovieTicketBooking.Infrastructure.Repositories;

namespace MovieTicketBooking.Application.Scenarious
{
    class ViewMovieBookings : IRunnable
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;

        public ViewMovieBookings(MovieRepository movieRepository, BookingRepository bookingRepository)
        {
            _movieRepository = movieRepository;
            _bookingRepository = bookingRepository;
        }

        public void Run()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter movie order in list");

                var movieNumber = int.Parse(Console.ReadLine());

                var movie = _movieRepository.GetMovieByIndex(movieNumber - 1);

                var booking = _bookingRepository.GetAll();

                Console.Clear();

                foreach (var item in booking)
                {
                    Console.WriteLine();

                    Console.WriteLine($"Name: {item}");
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
