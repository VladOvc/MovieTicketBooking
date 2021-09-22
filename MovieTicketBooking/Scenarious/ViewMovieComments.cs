using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Scenarious
{
    class ViewMovieComments : IRunnable
    {
        private List<Movie> _movies;
        private Movie _selectedMovie;

        public ViewMovieComments(List<Movie> movies)
        {
            _movies = movies;
        }
        public ViewMovieComments(List<Movie> movies, Movie selectedMovie)
        {
            _movies = movies;
            _selectedMovie = selectedMovie;
        }

        public void Run()
        {
            var selectedMovie = SelectMovie(_selectedMovie);

            foreach (var item in selectedMovie.Comments)
            {
                Console.WriteLine(item.Message);
            }

            Console.WriteLine("To return to the menu press BACKSPACE");
        }
        private Movie SelectMovie(Movie selectedMovie)
        {
            if (selectedMovie == null)
            {
                Console.WriteLine("Enter movie order in list");

                var movieNumber = int.Parse(Console.ReadLine());
                selectedMovie = _movies.ElementAt(movieNumber - 1);

                return selectedMovie;
            }
            else
            {
                return selectedMovie;
            }
        }
    }
}
