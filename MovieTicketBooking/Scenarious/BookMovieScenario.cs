﻿using MovieTicketBooking.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBooking.Scenarious
{
    public class BookMovieScenario : IRunnable
    {
        private List<Movie> _movies;
        private List<BookedTicket> _bookings;
        private string _pathToMoviesFile;
        private string _pathBookedTickets;
        private Movie _selectedMovie;

        public BookMovieScenario(List<Movie> movies, List<BookedTicket> bookings, string pathToMoviesFile, string pathBookedTickets)
        {
            _movies = movies;
            _bookings = bookings;
            _pathToMoviesFile = pathToMoviesFile;
            _pathBookedTickets = pathBookedTickets;
        }

        public BookMovieScenario(List<Movie> movies, List<BookedTicket> bookings, string pathToMoviesFile, string pathBookedTickets, Movie selectedMovie)
        {
            _movies = movies;
            _bookings = bookings;
            _pathToMoviesFile = pathToMoviesFile;
            _pathBookedTickets = pathBookedTickets;
            _selectedMovie = selectedMovie;
        }

        public void Run()
        {
            Console.WriteLine();

            try
            {
                /// Get movie

                var selectedMovie = SelectMovie(_selectedMovie);

                selectedMovie.ValidateAvailableSeats();

                Console.Clear();
                Console.WriteLine($"You have selected a movie called: {selectedMovie.Title}");

                /// Enter data

                Console.WriteLine("Enter your first name for booking");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter your last name for booking");
                string lastName = Console.ReadLine();

                Console.WriteLine("Enter your phone");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine("How many seats would you like to book");
                int numberToReserveSeats = int.Parse(Console.ReadLine());

                selectedMovie.BookRequestedSeats(numberToReserveSeats);

                _bookings.Add(new BookedTicket(selectedMovie.Id, firstName, lastName, phoneNumber, numberToReserveSeats));

                File.WriteAllText(_pathBookedTickets, JsonConvert.SerializeObject(_bookings, Formatting.Indented));
                File.WriteAllText(_pathToMoviesFile, JsonConvert.SerializeObject(_movies, Formatting.Indented));

            }
            catch (NotEnoughtSeatsEception exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (NoSeatsException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
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
