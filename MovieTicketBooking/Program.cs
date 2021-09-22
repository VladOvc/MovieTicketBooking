using MovieTicketBooking.Exceptions;
using MovieTicketBooking.Helpers;
using MovieTicketBooking.Repositories;
using MovieTicketBooking.Scenarious;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            var movieRepository = new MovieRepository();

            var uiHelper = new UIHelper(movieRepository);
            
            var pathBookedTickets = "../../../Files/BookedTickets.json";
            
            var bookedTicketsAssString = File.ReadAllText(pathBookedTickets);

            var bookings = JsonConvert.DeserializeObject<List<BookedTicket>>(bookedTicketsAssString);

            uiHelper.RenderMainMenu();

            /// Chouse menu item

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        Console.Clear();

                        uiHelper.RenderMoviesTable();
                        uiHelper.RenderMainMenu();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new SearchMovieScenario(movieRepository, bookings, pathBookedTickets).Run();

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        SortMovies();

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        new BookMovieScenario(movies, bookings, pathToMoviesFile, pathBookedTickets).Run();
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        new CancelBooking(movies, bookings, pathToMoviesFile, pathBookedTickets).Run();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        /*new ViewMovieReservation(movies, bookings).Run();*/
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        new ViewMovieComments(movies).Run();
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.X);
        }



        private static void SortMovies(List<Movie> movies, string pathToMoviesFile)
        {
            movies = movies.OrderBy(movie => movie.Title).ToList();

            Console.Clear();

            File.WriteAllText(pathToMoviesFile, JsonConvert.SerializeObject(movies, Formatting.Indented));

            RenderMoviesTable(movies);
            RenderMainMenu();
        }
    }
    public class Comment
    {
        public string Message { get; set; }

        public Comment(string message)
        {
            Message = message;
        }
    }
    public class Movie
    {
        public Guid Id = Guid.NewGuid();
        public string Title { get; set; }
        public int NumberOfFreeSeats { get; set; }
        public string Genre { get; set; }
        public float Rating { get; set; }
        public List<Comment> Comments { get; set; }

        public Movie(Guid id, string title, int numberOfFreeSeats, string genre, float rating)
        {
            Id = id;
            Title = title;
            NumberOfFreeSeats = numberOfFreeSeats;
            Genre = genre;
            Rating = rating;
            Comments = new List<Comment>();
        }

        internal void BookRequestedSeats(int requestedSeats)
        {
            if (NumberOfFreeSeats < requestedSeats)
            {
                throw new NotEnoughtSeatsEception("Our aplogies, there's no free seats available you need");
            }

            NumberOfFreeSeats = NumberOfFreeSeats - requestedSeats;
        }

        internal void ReturnBookingSeats(int returnedBooking)
        {
            NumberOfFreeSeats = NumberOfFreeSeats + returnedBooking;
        }

        internal void ValidateAvailableSeats()
        {
            if (NumberOfFreeSeats == 0)
            {
                throw new NoSeatsException($"There's no free seats for {Title}");
            }
        }
    }
    public class BookedTicket
    {
        public Guid MovieId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int FreeSeats { get; set; }
        public BookedTicket(Guid movieId, string firstName, string lastName, string phoneNumber, int numberOfReservedSeats)
        {
            MovieId = movieId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            FreeSeats = numberOfReservedSeats;
        }
    }
}
