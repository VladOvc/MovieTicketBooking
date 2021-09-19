using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Exceptions
{
    class NoBookedByPhoneForThisMovieException : Exception
    {
        public NoBookedByPhoneForThisMovieException(string message) : base(message)
        {

        }
    }
}
