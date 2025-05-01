using System;
using System.Collections;
using System.Collections.Generic;

namespace BookRentingApp
{
    public class Book: IComparable, IEquatable<Book>
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

        //IComparable CompareTo Implementation
        //Check if the books exactly match or order by author then title if they do not
        public int CompareTo(object? obj)
        {
            if (obj == null)
                return -1;

            Book b = (Book)obj;

            if (Author == b.Author && Title == b.Title && PrintDate == b.PrintDate && Genre == b.Genre
            && Popularity == b.Popularity && NumBooks == b.NumBooks && Priority == b.Priority && IsRead == b.IsRead)
                return 0;
            else
            {
                int retVal = Author.CompareTo(b.Author);
                if (retVal == 0)
                    retVal = Title.CompareTo(b.Title);
                return retVal;
            }
        }

        //Implement CompareTo that accepts a sort value to compare the books with
        public int CompareTo(object? obj, string sortValue)
        {
            if (obj == null)
                return -1;

            Book b = (Book)obj;

            switch (sortValue)
            {
                case "Author":
                    return Author.CompareTo(b.Author);
                case "Title":
                    return Title.CompareTo(b.Title);
                case "PrintDate":
                    return PrintDate.CompareTo(b.PrintDate);
                case "Genre":
                    return Genre.CompareTo(b.Genre);
                case "Popularity":
                    return Popularity.CompareTo(b.Popularity);
                case "NumBooks":
                    return NumBooks.CompareTo(b.NumBooks);
                case "Priority":
                    return Priority.CompareTo(b.Priority);
                case "IsRead":
                    return IsRead.CompareTo(b.IsRead);
                default:
                    return Title.CompareTo(b.Title);
            }
        }

        //override equals to return if two books are equal by comparing the author, title and print date
        public override bool Equals(Object? other)
        {
            if (other == null || !(other is Book))
                return false;
            Book? obj = (Book?)other;
            return obj != null && Author == obj.Author && Title == obj.Title && PrintDate == obj.PrintDate;
        }

        //implement equals to return if two books are equal by comparing author, title, and print date
        public bool Equals(Book? obj)
        {
            return obj != null && Author == obj.Author && Title == obj.Title && PrintDate == obj.PrintDate;
        }

        //override gethashcode to return a hashcode for the book based on the title and author
        public override int GetHashCode()
        {
            return (Title + Author).GetHashCode();
        }
    }
}
