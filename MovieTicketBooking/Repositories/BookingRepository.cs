using MovieTicketBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTicketBooking.Repositories
{
    public class BookingRepository
    {
        private FileContext _context;

        public BookingRepository()
        {
            _context = new FileContext();
        }

        public List<BookedTicket> GetAll()
        {
            return _context.Bookings;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        internal BookedTicket GetBookingByIndex(int index)
        {
            return _context.Bookings.ElementAt(index);
        }

        public void Delete(BookedTicket selectBooking)
        {
            _context.Bookings.Remove(selectBooking);

            Save();
        }

        public BookedTicket FindBookingByCriteria(string phoneNumberBooking, Movie selectedMovie)
        {
            var foundBooking = _context.Bookings.Where(item => item.PhoneNumber == phoneNumberBooking && item.MovieId == selectedMovie.Id).First();

            return foundBooking;
        }

        public List<BookedTicket> GetMovieById(Guid movieId)
        {
            _context.Bookings.Where(item => item.MovieId == movieId).ToList();

            return _context.Bookings;
        }

        public void AddNewBooking(Guid id, string firstName, string lastName, string phoneNumber, int numberToReserveSeats)
        {
            _context.Bookings.Add(new BookedTicket(id, firstName, lastName, phoneNumber, numberToReserveSeats));
        }
    }
}
