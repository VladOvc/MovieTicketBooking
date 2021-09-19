using System;


namespace MovieTicketBooking.Exceptions
{
    class NoBookingException : Exception
    {
        public NoBookingException(string message) : base(message)
        {

        }
    }
}
