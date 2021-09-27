using System;

using MovieTicketBooking.Domain.Entities;

namespace MovieTicketBooking.Application.Scenarious
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
