using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Scenarious
{
    class ViewMovieReservation : IRunnable
    {
        private List<BookedTicket> _bookings;
        private Movie _selectedMovie;

        public ViewMovieReservation(List<BookedTicket> bookings, Movie selectedMovie)
        {
            _bookings = bookings;
            _selectedMovie = selectedMovie;
        }

        public void Run()
        {
            var foundBookings = _bookings.Where(item => item.MovieId == _selectedMovie.Id).ToList();

            foreach (var item in foundBookings)
            {
                Console.WriteLine();

                Console.WriteLine(item.FirstName);
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.PhoneNumber);
                Console.WriteLine(item.FreeSeats);
            }
        }
    }
}
