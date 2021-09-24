using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Scenarious
{
    class ViewSpecificMovieComments : IRunnable
    {
        private Movie _selectedMovie;

        public ViewSpecificMovieComments( Movie selectedMovie)
        {
            _selectedMovie = selectedMovie;
        }

        public void Run()
        {
            Console.WriteLine();
            Console.WriteLine();

            foreach (var item in _selectedMovie.Comments)
            {
                Console.WriteLine(item.Message);
            }

            Console.WriteLine("To return to the menu press BACKSPACE");
        }
    }
}
