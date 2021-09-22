using MovieTicketBooking.Exceptions;
using MovieTicketBooking.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    public class BookSpecificMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;

        private Movie _specificMovie;

        private List<BookedTicket> _bookings;
        private string _pathBookedTickets;

        public BookSpecificMovieScenario(MovieRepository movieRepository, List<BookedTicket> bookings, string pathBookedTickets, Movie specificMovie)
        {
            _movieRepository = movieRepository;
            _bookings = bookings;
            _pathBookedTickets = pathBookedTickets;
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
                Console.WriteLine($"You have selected a movie called: {_specificMovie.Title}");

                /// Enter data

                Console.WriteLine("Enter your first name for booking");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter your last name for booking");
                string lastName = Console.ReadLine();

                Console.WriteLine("Enter your phone");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine("How many seats would you like to book");
                int numberToReserveSeats = int.Parse(Console.ReadLine());

                _specificMovie.BookRequestedSeats(numberToReserveSeats);

                _bookings.Add(new BookedTicket(_specificMovie.Id, firstName, lastName, phoneNumber, numberToReserveSeats));

                File.WriteAllText(_pathBookedTickets, JsonConvert.SerializeObject(_bookings, Formatting.Indented));
                _movieRepository.Save();

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
