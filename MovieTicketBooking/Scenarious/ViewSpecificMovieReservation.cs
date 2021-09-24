using MovieTicketBooking.Repositories;
using System;
using System.Collections.Generic;

namespace MovieTicketBooking.Scenarious
{
    class ViewSpecificMovieReservation : IRunnable
    {
        private BookingRepository _bookingRepository;
        private Movie _selectedMovie;

        public ViewSpecificMovieReservation(BookingRepository bookingRepository, Movie selectedMovie)
        {
            _bookingRepository = bookingRepository;
            _selectedMovie = selectedMovie;
        }

        public void Run()
        {
            Console.WriteLine();

            List<BookedTicket> foundBookings = _bookingRepository.FoundBookings(_selectedMovie);

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
