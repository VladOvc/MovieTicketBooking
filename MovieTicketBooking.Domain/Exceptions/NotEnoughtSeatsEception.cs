using System;

namespace MovieTicketBooking.Domain.Exceptions
{
    public class NotEnoughtSeatsEception : Exception
    {
        public NotEnoughtSeatsEception(string message) : base(message)
        {
        }
    }
}
