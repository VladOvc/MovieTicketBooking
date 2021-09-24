using System;

namespace MovieTicketBooking.Entities
{
    public class BookedTicket
    {
        public Guid MovieId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int FreeSeats { get; set; }

        public BookedTicket()
        {

        }
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
