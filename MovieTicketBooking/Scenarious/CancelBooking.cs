using MovieTicketBooking.Exceptions;
using MovieTicketBooking.Scenarious;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Scenarious
{
    public class CancelBooking : IRunnable
    {
        private List<Movie> _movies;
        private List<BookedTickets> _bookings;
        private string _pathToMoviesFile;
        private string _pathBookedTickets;

        public CancelBooking(List<Movie> movies, List<BookedTickets> bookings, string pathToMoviesFile, string pathBookedTickets)
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

                selectedMovie.ValidateAvailableSeats();

                Console.Clear();

                Console.WriteLine("your phone number on which the reservation was made");
                string phoneNumberBooking = Console.ReadLine();

                var foundBooking = SerchBooking(_bookings, phoneNumberBooking, selectedMovie);

                /// Delete booking 

                Console.WriteLine("Are you sure you want to delete your booked ticket?  Y/N");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Y:
                        Console.WriteLine();
                        Console.WriteLine("Your booking delted");
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine();
                        Console.WriteLine("To return to the menu press BACKSPACE");
                        return;
                    default:
                        break;
                }

                selectedMovie.ReturnBookingSeats(foundBooking.FreeSeats);

                _bookings.Remove(foundBooking);

                File.WriteAllText(_pathBookedTickets, JsonConvert.SerializeObject(_bookings, Formatting.Indented));
                File.WriteAllText(_pathToMoviesFile, JsonConvert.SerializeObject(_movies, Formatting.Indented));
            }
            catch (NoBookedByPhoneForThisMovieException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (NoBookedByPhoneNumberException exception)
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

        private BookedTickets SerchBooking(List<BookedTickets> bookings, string phoneNumberBooking, Movie selectedMovie)
        {
            var foundBooking = bookings.Find(item => item.PhoneNumber == phoneNumberBooking);

            if (foundBooking == null)
            {
                throw new NoBookedByPhoneNumberException("We could not find your booked ticket with your phone number.");
            }
            else
            {
                if (foundBooking.MovieId == selectedMovie.Id)
                {
                    return foundBooking;
                }
                throw new NoBookedByPhoneForThisMovieException("We could not find a booked ticket for this movie using your phone number.");
            }
        }
    }
}
