using System;
using System.Collections.Generic;

using MovieTicketBooking.Domain.Entities;
using MovieTicketBooking.Infrastructure.Repositories;

namespace MovieTicketBooking.Application.Scenarious
{
    class ViewSpecificMovieBooking : IRunnable
    {
        private BookingRepository _bookingRepository;
        private Movie _movie;

        public ViewSpecificMovieBooking(BookingRepository bookingRepository, Movie movie)
        {
            _bookingRepository = bookingRepository;
            _movie = movie;
        }

        public void Run()
        {
            Console.WriteLine();

            List<BookedTicket> bookings = _bookingRepository.GetMovieById(_movie.Id);

            foreach (var item in bookings)
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
