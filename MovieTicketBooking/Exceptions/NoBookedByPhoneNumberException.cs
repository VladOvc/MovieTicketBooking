using System;

namespace MovieTicketBooking.Exceptions
{
    class NoBookedByPhoneNumberException : Exception
    {
        public NoBookedByPhoneNumberException(string message) : base(message)
        {

        }
    }
}
