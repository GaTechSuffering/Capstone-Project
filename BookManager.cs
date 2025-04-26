using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRentingApp
{
    public class BookManager
    {
        public int DaysRemaining { get; set; }
        public int Price { get; set; }
        
        // Method to rent a book
        public void RentBook(string bookTitle, int rentalDays)
        {
            if (DaysRemaining > 0)
            {
                Console.WriteLine($"{bookTitle} is already rented. {DaysRemaining} day(s) left.");
            }
            else
            {
                DaysRemaining = rentalDays;
                Console.WriteLine($"\"{bookTitle}\" is being rented for {rentalDays} day(s).");
            }
        }

        // Check if the book is late and calculate penalty
        public int CalculateLateFee()
        {
            return DaysRemaining < 0 ? -DaysRemaining * Price : 0;
        }
    }
}