using MovieTicketBooking.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Helpers
{
    class UIHelper
    {

        private MovieRepository _movieRepository;

        public UIHelper(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void RenderMoviesTable()
        {
            var movies = _movieRepository.GetAll();

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

        public void RenderMainMenu()
        {
            /// Render main menu

            Console.WriteLine();

            Console.WriteLine("1 Search a movie");
            Console.WriteLine("2 Sort a movies");
            Console.WriteLine("3 Book a Ticket");
            Console.WriteLine("4 Booking List");
            Console.WriteLine("5 Cancel Booking");
            Console.WriteLine("*-6 Add a new movie-*");
            Console.WriteLine("7 View movie coments");
        }

    }
}
