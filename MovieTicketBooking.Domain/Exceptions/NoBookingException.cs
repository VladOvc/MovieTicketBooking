using System;


namespace MovieTicketBooking.Domain.Exceptions
{
    class NoBookingException : Exception
    {
        public NoBookingException(string message) : base(message)
        {

        }
    }
}
