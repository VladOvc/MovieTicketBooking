using MovieTicketBooking.Exceptions;
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
            var pathToMoviesFile = "../../../Files/Movies.json";
            var pathBookedTickets = "../../../Files/BookedTickets.json";

            var moviesAssString = File.ReadAllText(pathToMoviesFile);
            var bookedTicketsAssString = File.ReadAllText(pathBookedTickets);

            var movies = JsonConvert.DeserializeObject<List<Movie>>(moviesAssString);
            var bookings = JsonConvert.DeserializeObject<List<BookedTicket>>(bookedTicketsAssString);

            RenderMoviesTable(movies);
            RenderMainMenu();

            /// Chouse menu item

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        Console.Clear();

                        RenderMoviesTable(movies);
                        RenderMainMenu();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new SearchMovieScenario(movies, bookings, pathToMoviesFile, pathBookedTickets).Run();

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        SortMovies(movies, pathToMoviesFile);

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

        private static void RenderMainMenu()
        {
            /// Render main menu

            Console.WriteLine();

            Console.WriteLine("1 Search a movie");
            Console.WriteLine("2 Sort a movies");
            Console.WriteLine("3 Book a Ticket");
            Console.WriteLine("4 Booking List");
            Console.WriteLine("5 Cancel Booking");
            Console.WriteLine("6 Add a new movie");
        }

        private static void RenderMoviesTable(List<Movie> movies)
        {
            /// Render a table

            var maxTitleLenght = movies.Max(asd => asd.Title.Length);

            var titleColumnName = "Title";

            var rightPaddingTitle = new string(' ', maxTitleLenght - titleColumnName.Length);

            Console.WriteLine($"| ## | {titleColumnName}{rightPaddingTitle} | ## |");

            foreach (var movieIterator in movies.Select((item, index) => (item, index)))
            {
                var rightPadding = new string(' ', maxTitleLenght - movieIterator.item.Title.Length);

                var number = movieIterator.index + 1;

                Console.WriteLine($"| {number.ToString("D2")} | {movieIterator.item.Title}{rightPadding} | {movieIterator.item.NumberOfFreeSeats} |");
            }
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

        public Movie(Guid id, string title, int numberOfFreeSeats, string genre, float rating,List<Comment> comments)
        {
            Id = id;
            Title = title;
            NumberOfFreeSeats = numberOfFreeSeats;
            Genre = genre;
            Rating = rating;
            Comments = comments;
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
