using System;

using MovieTicketBooking.Domain.Entities;
using MovieTicketBooking.Domain.Exceptions;
using MovieTicketBooking.Infrastructure.Repositories;

namespace MovieTicketBooking.Application.Scenarious
{
    public class BookingSpecificMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;
        private Movie _specificMovie;

        public BookingSpecificMovieScenario(MovieRepository movieRepository, BookingRepository bookingRepository, Movie specificMovie)
        {
            _movieRepository = movieRepository;
            _bookingRepository = bookingRepository;
            _specificMovie = specificMovie;
        }

        public void Run()
        {
            Console.WriteLine();

            try
            {
                /// Get movie

                _specificMovie.ValidateAvailableSeats();

                Console.Clear();
                Console.WriteLine($"_____Booking <{_specificMovie.Title}> Movie Scenario_____");
                Console.WriteLine();
                Console.WriteLine($"You have selected a movie called: {_specificMovie.Title}");

                /// Enter data

                Console.WriteLine("Enter your first name for booking");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter your last name for booking");
                string lastName = Console.ReadLine();

                Console.WriteLine("Enter your phone");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine($"How many seats would you like to book <Free sets {_specificMovie.NumberOfFreeSeats}>");
                int numberToReserveSeats = int.Parse(Console.ReadLine());

                _specificMovie.BookRequestedSeats(numberToReserveSeats);

                _bookingRepository.AddNewBooking(_specificMovie.Id, firstName, lastName, phoneNumber, numberToReserveSeats);

                _bookingRepository.Save();
                _movieRepository.Save();

                Console.WriteLine();
                Console.WriteLine($"Your reservation for the movie {_specificMovie.Title} was booked successfully");
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
