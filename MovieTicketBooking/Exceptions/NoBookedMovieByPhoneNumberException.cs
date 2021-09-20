using System;

namespace MovieTicketBooking.Exceptions
{
    class NoBookedMovieByPhoneNumberException : Exception
    {
        public NoBookedMovieByPhoneNumberException(string message) : base(message)
        {

        }
    }
}
