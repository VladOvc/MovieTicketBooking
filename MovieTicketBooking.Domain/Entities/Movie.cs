using MovieTicketBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace MovieTicketBooking.Domain.Entities
{
    public class Movie
    {
        public Guid Id = Guid.NewGuid();
        public string Title { get; set; }
        public int NumberOfFreeSeats { get; set; }
        public string Genre { get; set; }
        public float Rating { get; set; }
        public List<Comment> Comments { get; set; }

        public Movie()
        {

        }

        public Movie(Guid id, string title, int numberOfFreeSeats, string genre, float rating)
        {
            Id = id;
            Title = title;
            NumberOfFreeSeats = numberOfFreeSeats;
            Genre = genre;
            Rating = rating;
            Comments = new List<Comment>();
        }

        public void BookRequestedSeats(int requestedSeats)
        {
            if (NumberOfFreeSeats < requestedSeats)
            {
                throw new NotEnoughtSeatsEception("Our aplogies, there's no free seats available you need");
            }

            NumberOfFreeSeats = NumberOfFreeSeats - requestedSeats;
        }

        public void ReturnBookingSeats(int returnedBooking)
        {
            NumberOfFreeSeats = NumberOfFreeSeats + returnedBooking;
        }

        public void ValidateAvailableSeats()
        {
            if (NumberOfFreeSeats == 0)
            {
                throw new NoSeatsException($"There's no free seats for {Title}");
            }
        }
    }
    public class Comment
    {
        public string User { get; set; }
        public string Message { get; set; }

        public Comment()
        {

        }

        public Comment(string user, string message)
        {
            User = user;
            Message = message;
        }
    }
}
