using System;
using System.Linq;
using System.Collections.Generic;

namespace BookRentingApp
{
    class Program
    {
        static BookManager bookManager = new BookManager();
        static UserAccount currUser;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to the Book Rental System ===");

            // Set up a dummy account to use 
            List<Book> ownedBooks = BookLoader.LoadBooksFromFile("ownedBooks.txt");
            List<Book> wishlistBooks = BookLoader.LoadBooksFromFile("wishListBooks.txt");
            currUser = new UserAccount("John", "Doe", 29, true, 0, ownedBooks, wishlistBooks);
            // Assign to user's owned books
            currUser.OwnedBooks.AddRange(ownedBooks);
             // Assign to user's wishlisted books
            currUser.Wishlist.AddRange(wishlistBooks);

            while (true)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Owned Books");
                Console.WriteLine("2. Finished Books");
                Console.WriteLine("3. Wishlist Books");
                Console.WriteLine("4. Rent Books");
                Console.WriteLine("5. Recommendations");
                Console.WriteLine("0. Exit");

                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": OwnedBooksMenu(); break;
                    case "2": FinishedBooksMenu();  break;
                    case "3": WishlistBooksMenu(); break;
                    case "4": RentBooksMenu(); break;
                    case "5": ShowRecommendations(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void OwnedBooksMenu()
        {
            var owned = currUser.OwnedBooks.ToList();
            SortBookList(owned);
            DisplayBookList(owned);
        }

        static void FinishedBooksMenu()
        {
            var finished = currUser.OwnedBooks.Where(b => b.IsRead).ToList();
            SortBookList(finished);
            DisplayBookList(finished);
        }

        //     Console.Write("Toggle isRead status (title): ");
        //     string title = Console.ReadLine();
        //     var book = libraryMenu.OwnedBooks.FirstOrDefault(b => b.Title == title);
        //     if (book != null)
        //     {
        //         book.IsRead = !book.IsRead;
        //         Console.WriteLine($"'{book.Title}' is now marked as {(book.IsRead ? "read" : "unread")}.");
        //     }

        static void WishlistBooksMenu()
        {
            var wishlist = currUser.Wishlist;
            QuickSort.SortBooks(wishlist, "Priority");
            
            Console.WriteLine("Would you like to order this list by title or author? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
                SortBookList(wishlist);
            
            DisplayBookList(wishlist);

            Console.Write("Would you like to add a book to your wishlist? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.Write("Author: ");
                string author = Console.ReadLine();
                Console.Write("Title: ");
                string title = Console.ReadLine();
                Console.Write("Print Date (type 0 if you do not know): ");
                string printDate = Console.ReadLine();
                Console.Write("Genre: ");
                string genre = Console.ReadLine();
                Console.Write("Priority (1 is the highest): ");
                int priority = int.Parse(Console.ReadLine());
                var newBook = new Book(author, title, printDate, genre, -1, -1, priority, false);
                currUser.Wishlist.Add(newBook);
            }
        }

        static void RentBooksMenu()
        {
            Console.Write("Enter book title to rent: ");
            string title = Console.ReadLine();
            Console.Write("How many days?: ");
            int days = int.Parse(Console.ReadLine());

            // Members get priority
            if (currUser.IsMember)
                Console.WriteLine("Congratulations, you're a member! You are renting with priority access.");
            else
                Console.WriteLine("You're a guest. Renting will be based on availability.");

            bookManager.RentBook(title, days);
        }

        static void ShowRecommendations()
        {
        //     var allBooks = libraryMenu.OwnedBooks.Concat(libraryMenu.Wishlist);

        //     var popular = allBooks.Where(b => b.Popularity > 7);
        //     var genre = allBooks.Where(b => b.Genre == "Fantasy");
        //     var byAuthor = allBooks.Where(b => b.Authors.Any(a => a.LastName == "Tolkien"));

        //     Console.WriteLine("Recommendations by Popularity:");
        //     DisplayBookList(popular.ToList());

        //     Console.WriteLine("Recommendations by Genre (Fantasy):");
        //     DisplayBookList(genre.ToList());

        //     Console.WriteLine("Recommendations by Author (Tolkien):");
        //     DisplayBookList(byAuthor.ToList());
        }

        // Helper function to sort/organise the list of books
        static void SortBookList(List<Book> sortList) {
            Console.WriteLine("Sort by: 1) Title 2) Author");
            string sort = Console.ReadLine();

            if (sort == "1")
                QuickSort.SortBooks(sortList, "Title");
            else if (sort == "2")
                QuickSort.SortBooks(sortList, "Author");

            Console.WriteLine("Order: 1) Ascending 2) Descending");
            string order = Console.ReadLine();
            if (order == "2")
                sortList.Reverse();
        }

        // Helper function to display the list of books
        static void DisplayBookList(List<Book> books)
        {
            foreach (var book in books)
                Console.WriteLine($"☐ {book.Title} by {string.Join(", ", book.Author)} \n\tGenre: {book.Genre} \n\tPopularity: {book.Popularity}");
        }
    }
}
