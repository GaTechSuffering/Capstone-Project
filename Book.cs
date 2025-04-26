using System;
using System.Collections.Generic;

namespace BookRentingApp
{
    public class Book
    {
        // Properties
        public string Author { get; set; }
        public string Title { get; set; }
        public string PrintDate { get; set; }
        public string Genre { get; set; }
        public int Popularity { get; set; }

        // Availability in the number of books
        public int NumBooks { get; set; }
        // The priority of the book; 1 is the top priority
        public int Priority { get; set; }
        // Whether or not the book has already been read
        public bool IsRead { get; set; }

        // Constructor
        public Book(string author, string title, string printDate, string genre, int popularity, int numBooks, int priority, bool isRead)
        {
            Author = author;
            Title = title;
            PrintDate = printDate;
            Genre = genre;
            Popularity = popularity;
            NumBooks = numBooks;
            Priority = priority; 
            IsRead = isRead;
        }

        // Display book info
        public void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Printed: {PrintDate}");
            Console.WriteLine($"Genre: {Genre}");
            Console.WriteLine($"Popularity: {Popularity}");
            Console.WriteLine($"Available Copies: {NumBooks}");
            Console.WriteLine($"Priority in Queue: {Priority}");
        }
    }
}
