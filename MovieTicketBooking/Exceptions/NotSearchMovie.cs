using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Exceptions
{
    class NotSearchMovie : Exception
    {
        public NotSearchMovie(string message) : base(message)
        {

        }
    }
}
