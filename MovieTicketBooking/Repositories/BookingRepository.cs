using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Repositories
{
    public class BookingRepository
    {
        private List<BookedTicket> _bookings;

        private string _pathToBookings = "../../../Files/BookedTickets.json";

        public BookingRepository()
        {
            _bookings = JsonConvert.DeserializeObject<List<BookedTicket>>(File.ReadAllText(_pathToBookings));
        }

        public List<BookedTicket> GetAll()
        {
            return _bookings;
        }

        public void Save()
        {
            File.WriteAllText(_pathToBookings, JsonConvert.SerializeObject(_bookings, Formatting.Indented));
        }

        public void Delete(BookedTicket selectBooking)
        {
            _bookings.Remove(selectBooking);

            Save();
        }

        public BookedTicket FindBooking(string phoneNumberBooking, Movie selectedMovie)
        {
            var foundBooking = _bookings.Where(item => item.PhoneNumber == phoneNumberBooking && item.MovieId == selectedMovie.Id).First();

            return foundBooking;
        }

        public List<BookedTicket> FoundBookings(Movie selectedMovie)
        {
            _bookings.Where(item => item.MovieId == selectedMovie.Id).ToList();

            return _bookings;
        }

        public void AddNewBooking(Guid id, string firstName, string lastName, string phoneNumber, int numberToReserveSeats)
        {
            _bookings.Add(new BookedTicket(id, firstName, lastName, phoneNumber, numberToReserveSeats));
        }
    }
}
