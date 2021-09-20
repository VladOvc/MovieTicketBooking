using MovieTicketBooking.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    public class CancelBooking : IRunnable
    {
        private List<Movie> _movies;
        private List<BookedTicket> _bookings;
        private string _pathToMoviesFile;
        private string _pathBookedTickets;

        public CancelBooking(List<Movie> movies, List<BookedTicket> bookings, string pathToMoviesFile, string pathBookedTickets)
        {
            _movies = movies;
            _bookings = bookings;
            _pathToMoviesFile = pathToMoviesFile;
            _pathBookedTickets = pathBookedTickets;
        }

        public void Run()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("What movie was the booking tickets for");

                var movieNumber = int.Parse(Console.ReadLine());
                var selectedMovie = _movies.ElementAt(movieNumber - 1);

                Console.Clear();

                Console.WriteLine("your phone number on which the reservation was made");
                string phoneNumberBooking = Console.ReadLine();

                var foundBooking = SerchBooking(_bookings, phoneNumberBooking, selectedMovie);

                /// Render your booking

                RenderBookingTickets(foundBooking, selectedMovie);

                /// Delete booking 

                Console.WriteLine("Are you sure you want to delete your booked ticket?  Y/N");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Y:

                        selectedMovie.ReturnBookingSeats(foundBooking.FreeSeats);

                        _bookings.Remove(foundBooking);

                        File.WriteAllText(_pathBookedTickets, JsonConvert.SerializeObject(_bookings, Formatting.Indented));
                        File.WriteAllText(_pathToMoviesFile, JsonConvert.SerializeObject(_movies, Formatting.Indented));

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
            catch (NoBookedMovieByPhoneNumberException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (NoBookingException exception)
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

        private BookedTicket SerchBooking(List<BookedTicket> bookings, string phoneNumberBooking, Movie selectedMovie)
        {
            var foundBooking = bookings.Where(item => item.PhoneNumber == phoneNumberBooking && item.MovieId == selectedMovie.Id).FirstOrDefault();

            if (foundBooking == null)
            {
                throw new NoBookedMovieByPhoneNumberException("We could not find your booked ticket with your phone number.");
            }
            return foundBooking;
        }
    }
}
