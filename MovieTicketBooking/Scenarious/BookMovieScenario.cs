using MovieTicketBooking.Exceptions;
using MovieTicketBooking.Repositories;
using System;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    public class BookMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;

        public BookMovieScenario(MovieRepository movieRepository, BookingRepository bookingRepository)
        {
            _movieRepository = movieRepository;
            _bookingRepository = bookingRepository;

        }

        public void Run()
        {
            Console.WriteLine();

            try
            {
                /// Get movie

                Console.WriteLine("Enter movie order in list");

                var movieNumber = int.Parse(Console.ReadLine());
                var selectedMovie = _movieRepository.GetAll().ElementAt(movieNumber - 1);

                selectedMovie.ValidateAvailableSeats();

                Console.Clear();
                Console.WriteLine($"_____Booking <{selectedMovie.Title}> Movie Scenario_____");
                Console.WriteLine();
                Console.WriteLine($"You have selected a movie called: {selectedMovie.Title}");
                Console.WriteLine();

                /// Enter data

                Console.WriteLine("Enter your first name for booking");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter your last name for booking");
                string lastName = Console.ReadLine();

                Console.WriteLine("Enter your phone");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine($"How many seats would you like to book <Free seats {selectedMovie.NumberOfFreeSeats}>");
                int numberToReserveSeats = int.Parse(Console.ReadLine());

                selectedMovie.BookRequestedSeats(numberToReserveSeats);

                _movieRepository.Save();

                _bookingRepository.AddNewBooking(selectedMovie.Id, firstName, lastName, phoneNumber, numberToReserveSeats);

            }
            catch (NotEnoughtSeatsEception exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (NoSeatsException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine("To return to the menu press BACKSPACE");
        }
    }
}
