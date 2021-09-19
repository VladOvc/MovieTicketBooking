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
            var bookings = JsonConvert.DeserializeObject<List<BookedTickets>>(bookedTicketsAssString);

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
                        new CancelBooking(movies, bookings, pathToMoviesFile, pathBookedTickets);
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

            var content = JsonConvert.SerializeObject(movies);

            File.WriteAllText(pathToMoviesFile, content);

            RenderMoviesTable(movies);
            RenderMainMenu();
        }
    }

    public class Movie
    {
        public Guid Id = Guid.NewGuid();
        public string Title { get; set; }
        public int NumberOfFreeSeats { get; set; }

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
    public class BookedTickets
    {
        public Guid MovieId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int FreeSeats { get; set; }
        public BookedTickets(Guid movieId, string firstName, string lastName, string phoneNumber, int numberOfReservedSeats)
        {
            MovieId = movieId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            FreeSeats = numberOfReservedSeats;
        }
    }
}
