using System;

using MovieTicketBooking.Infrastructure.Repositories;
using MovieTicketBooking.Domain.Entities;
using MovieTicketBooking.Domain.Exceptions;

namespace MovieTicketBooking.Application.Scenarious
{
    public class CancelBooking : IRunnable
    {
        private MovieRepository _movieRepository;
        private BookingRepository _bookingRepository;

        public CancelBooking(MovieRepository movieRepository, BookingRepository bookingRepository)
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
                Console.WriteLine("What movie was the booking tickets for");

                var movieNumber = int.Parse(Console.ReadLine());
                var selectedMovie = _movieRepository.GetMovieByIndex(movieNumber - 1);

                Console.Clear();

                Console.WriteLine("your phone number on which the reservation was made");
                string phoneNumberBooking = Console.ReadLine();

                var foundBooking = _bookingRepository.FindBookingByCriteria(phoneNumberBooking, selectedMovie);

                /// Render your booking

                RenderBookingTickets(foundBooking, selectedMovie);

                /// Delete booking 

                Console.WriteLine("Are you sure you want to delete your booked ticket?  Y/N");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Y:

                        selectedMovie.ReturnBookingSeats(foundBooking.FreeSeats);

                        _movieRepository.Save();

                        _bookingRepository.Delete(foundBooking);

                        Console.WriteLine();
                        Console.WriteLine("Your booking deleted");
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine();
                        Console.WriteLine("To return to the menu press BACKSPACE");
                        return;
                    default:
                        break;
                }
            }
            catch (NoSeatsException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine();
                Console.WriteLine("booking was not found");

                ConsoleKeyInfo KeyInfo = Console.ReadKey();

                switch (KeyInfo.Key)
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

            Console.WriteLine("To return to the menu press BACKSPACE");
        }

        private void RenderBookingTickets(BookedTicket foundBooking, Movie selectedMovie)
        {
            Console.WriteLine();

            Console.WriteLine("We found your booked ticket))");

            Console.WriteLine();
            Console.WriteLine($"Movie:      {selectedMovie.Title}");
            Console.WriteLine($"First name: {foundBooking.FirstName}");
            Console.WriteLine($"Last name:  {foundBooking.LastName}");
            Console.WriteLine($"Phone:      {foundBooking.PhoneNumber}");
            Console.WriteLine();
        }
    }
}
