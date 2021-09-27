using System;
using System.Linq;
using System.Collections.Generic;

using MovieTicketBooking.Domain.Entities;

namespace MovieTicketBooking.Infrastructure.Repositories
{
    public class MovieRepository
    {
        private FileContext _context;

        public MovieRepository()
        {
            _context = new FileContext();
        }

        public List<Movie> GetAll()
        {
            return _context.Movies;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Movie FindMovieByCriteria(string titleToSearch, string specifier)
        {
            var foundMovie = _context.Movies.Where(item => item.Title.ToLower().Contains(titleToSearch.ToLower())
                                                    || item.Genre.ToLower().Contains(titleToSearch.ToLower())
                                                    || item.Rating.ToString(specifier) == titleToSearch)
                                                    .First();
            return foundMovie;
        }

        public Movie GetMovieByIndex(int index)
        {
            return _context.Movies.ElementAt(index);
        }
        public void AddNewMovie(Guid id, string titleMovie, int NumberOfFreeSeatsMovie, string genreMovie, float ratingMovie)
        {
            _context.Movies.Add(new Movie(id, titleMovie, NumberOfFreeSeatsMovie, genreMovie, ratingMovie));

            Save();
        }


        /* public void SortMovieTitle()
         {
             _movies = _movies.OrderBy(movie => movie.Title).ToList();

             Save();
         }

         public void SortMovieGenre()
         {
             _movies = _movies.OrderBy(movie => movie.Genre).ToList();

             Save();
         }

         public void SortMovieRating()
         {
             _movies = _movies.OrderBy(movie => movie.Rating).ToList();

             Save();
         }*/

/*        public List<Movie> GetPage(int page, int pageSize)
        {
            double maxPages = Math.Round((double)(_movies.Count() / pageSize), MidpointRounding.ToEven);

            return _movies.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }*/
    }
}
