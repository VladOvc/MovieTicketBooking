using MovieTicketBooking.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MovieTicketBooking.Repositories
{
    public class FileContext
    {

        private readonly string _pathToBookings = "../../../Files/BookedTickets.json";
        private readonly string _pathToMovies = "../../../Files/Movies.json";

        private readonly List<BookedTicket> _bookings;
        private readonly List<Movie> _movies;

        public List<BookedTicket> Bookings
        {
            get
            {
                return _bookings;
            }

        }
        public List<Movie> Movies
        {
            get
            {
                return _movies;
            }
        }
        public FileContext()
        {
            _bookings = JsonConvert.DeserializeObject<List<BookedTicket>>(File.ReadAllText(_pathToBookings));
            _movies = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(_pathToMovies));
        }

        public void SaveChanges()
        {
            File.WriteAllText(_pathToBookings, JsonConvert.SerializeObject(_bookings, Formatting.Indented));
            File.WriteAllText(_pathToMovies, JsonConvert.SerializeObject(_movies, Formatting.Indented));
        }
    }
}
