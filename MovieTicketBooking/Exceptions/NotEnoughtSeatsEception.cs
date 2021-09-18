using System;

namespace MovieTicketBooking.Exceptions
{
    public class NotEnoughtSeatsEception : Exception
    {
        public NotEnoughtSeatsEception(string message) : base(message)
        {
        }
    }
}
