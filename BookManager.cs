using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRentingApp
{
    public class BookManager
    {
        public int DaysRemaining { get; set; }
        public int Price { get; set; }
        public UserAccount? CurrentRenter { get; private set; }
        
        // Method to rent a book
        public void RentBook(UserAccount user, int rentalDays)
        {
            Book lastBook = user.Rented[^1];
            string bookTitle = lastBook.Title;

            if (DaysRemaining > 0)
            {
                Console.WriteLine($"{DaysRemaining} day(s) remaining.");
            }
            else
            {
                DaysRemaining = rentalDays;
                CurrentRenter = user;
                Console.WriteLine($"\"{bookTitle}\" is being rented for {rentalDays} day(s) by {user.FirstName}.");

                if (user.IsMember)
                    Console.WriteLine("Thank you for being a valued member!");
            }
        }

        // Method to simulate passage of time or returns
        public void AdvanceDay(string bookTitle)
        {
            if (DaysRemaining > 0)
            {
                DaysRemaining--;

                if (DaysRemaining == 0)
                {
                    Console.WriteLine($"Book \"{bookTitle}\" is now available.");
                    CurrentRenter = null;
                }
            }
        }

        // Check if the book is late and calculate penalty
        public int CalculateLateFee()
        {
            return DaysRemaining < 0 ? -DaysRemaining * Price : 0;
        }
    }
}