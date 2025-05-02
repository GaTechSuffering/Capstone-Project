using System;
using System.Linq;
using System.Collections.Generic;

namespace BookRentingApp
{
    class Program
    {
        static BookManager bookManager = new BookManager();
        static UserAccount? currUser, currSelect;
        static Dictionary <string, UserAccount> rentalSystem = new Dictionary <string, UserAccount>();
        static BookRelationshipGraph bookRelationships = new BookRelationshipGraph();

        static void Main(string[] args)
        {
            TreeNode node = BuildTree();

            Console.WriteLine("=== Welcome to the Book Rental System ===\n");

            // Set up two dummy accounts for the rentalSystem
            List<Book> ownedBooks = BookLoader.LoadBooksFromFile("ownedBooks_John.txt");
            List<Book> wishlistBooks = BookLoader.LoadBooksFromFile("wishListBooks_John.txt");
            List<Book> rentedBooks = new List<Book> ();
            currUser = new UserAccount("John", "Doe", 29, true, 10, ownedBooks, wishlistBooks, rentedBooks);
            // Assign to user's owned books
            currUser.OwnedBooks.AddRange(ownedBooks);
            // Assign to user's wishlisted books
            currUser.Wishlist.AddRange(wishlistBooks);
            string userName = "JohnnyBoy";

            // Add user to the rental system
            rentalSystem.Add(userName, currUser);

            // Secondary dummy account
            ownedBooks = BookLoader.LoadBooksFromFile("ownedBooks_Jane.txt");
            wishlistBooks = BookLoader.LoadBooksFromFile("wishListBooks_Jane.txt");
            currUser = new UserAccount("Jane", "Darling", 25, false, 0, ownedBooks, wishlistBooks, rentedBooks);
            // Assign to user's owned books
            currUser.OwnedBooks.AddRange(ownedBooks);
            // Assign to user's wishlisted books
            currUser.Wishlist.AddRange(wishlistBooks);
            userName = "SuperHeroJane";

            rentalSystem.Add(userName, currUser);

            // Force the user to select an account first 
            SelectUser();

            // Loop through main menu until user selects "Exit"
            while (true)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Select User");
                Console.WriteLine("2. Display User Information");
                Console.WriteLine("3. Owned Books");
                Console.WriteLine("4. Finished Books");
                Console.WriteLine("5. Wishlist Books");
                Console.WriteLine("6. Rent Books");
                Console.WriteLine("7. Recommendations");
                Console.WriteLine("0. Exit");

                Console.Write("Choose an option: ");
                string input = Console.ReadLine() ?? "";

                switch (input)
                {
                    case "1": SelectUser(); break;
                    case "2": DisplayUser(); break;
                    case "3": OwnedBooksMenu(); break;
                    case "4": FinishedBooksMenu();  break;
                    case "5": WishlistBooksMenu(); break;
                    case "6": RentBooksMenu(); break;
                    case "7": ShowRecommendations(node); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        // Selects the user from the current dictionary (rental system)
        static void SelectUser()
        {
            Console.WriteLine("Which user are you trying to access?");

            if (rentalSystem.Count == 0)
            {
                Console.WriteLine("The dictionary is empty.");
                return;
            }
    
            foreach (KeyValuePair<string, UserAccount> user in rentalSystem)
                Console.WriteLine($"☐  Username: \"{user.Key}\" \n   User Information: \n\t{user.Value}");
    
            string inputUsername;
            while (true)
            {
                Console.Write("\nEnter a valid username: ");
                inputUsername = Console.ReadLine() ?? "";
    
                if (rentalSystem.ContainsKey(inputUsername))
                {
                    Console.WriteLine("User " + inputUsername + " selected!");
                    currSelect = rentalSystem[inputUsername];
                    UserQueueManager.EnqueueUser(currSelect);
                    break;
                }
                else
                    Console.WriteLine("Invalid username. Please try again.");
            }
        }
        
        static void DisplayUser() {
            if (currSelect != null)
                currSelect.DisplayInfo();
            else
                Console.WriteLine("No user selected.");
        }

        // Display the list of owned books to the currently selected user
        static void OwnedBooksMenu()
        {
            if (currSelect != null)
            {
                var owned = currSelect.OwnedBooks.ToList();
                SortBookList(owned);
                DisplayBookList(owned);

                // Check if they want to mark any of these books as completed and do so
                string response;
                do
                {
                    Console.WriteLine("Would you like to mark a book as finished? (y/n): ");
                    response = (Console.ReadLine() ?? "").Trim().ToLower();
                } while (response != "y" && response != "n");

                if (response == "y")
                {
                    try
                    {
                        Console.WriteLine("Which book would you like to mark as finished? (enter [title] by [author]): ");
                        string input = Console.ReadLine() ?? "";
                        string[] finishedBook = input.Split(" by ");
                
                        // Validate format
                        if (finishedBook.Length != 2 || string.IsNullOrWhiteSpace(finishedBook[0]) || string.IsNullOrWhiteSpace(finishedBook[1]))
                        {
                            Console.WriteLine("Invalid format. Please enter the book as: [title] by [author]");
                            return;
                        }
                
                        string title = finishedBook[0].Trim();
                        string author = finishedBook[1].Trim();
                
                        // Validate that exactly one match exists
                        var match = currSelect.OwnedBooks
                            .FirstOrDefault(x => x.Author == author && x.Title == title);
                
                        if (match != null)
                        {
                            if (currSelect.FirstName == "John")
                                ListUpdater.UpdateBookList(match, "ownedBooks_John.txt");
                            else if (currSelect.FirstName == "Jane")
                                ListUpdater.UpdateBookList(match, "ownedBooks_Jane.txt");
                
                            match.IsRead = true;
                            Console.WriteLine("The book has been updated.");
                        }
                        else
                        {
                            Console.WriteLine("The book entered was not found in your collection.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }

                // Check if they want to add to the list of owned books and do so
                do
                {
                    Console.WriteLine("Would you like to add a book to owned books? (y/n): ");
                    response = (Console.ReadLine() ?? "").Trim().ToLower();
                } while (response != "y" && response != "n");

                if (response == "y")
                    AddBook("owned", false);
            } 
            else 
                Console.WriteLine("No user is currently selected.");
        }

        // Display the list of finished books to the currently selected user
        static void FinishedBooksMenu()
        {
            if (currSelect != null)
            {
                var finished = currSelect.OwnedBooks.Where(b => b.IsRead).ToList();
                SortBookList(finished);
                DisplayBookList(finished);

                // Check if they want to add to the list of owned books and do so
                string response;
                do
                {
                    Console.WriteLine("Would you like to add a new book to finished books? (y/n): ");
                    response = (Console.ReadLine() ?? "").Trim().ToLower();
                } while (response != "y" && response != "n");

                if (response == "y")
                    AddBook("owned", true);
            } 
            else
                Console.WriteLine("No user is currently selected.");
        }

        // Display the list of wishlisted books to the currently selected user
        static void WishlistBooksMenu()
        {
            if (currSelect != null)
            {
                var wishlist = currSelect.Wishlist;
                QuickSort.SortBooks(wishlist, "Priority");

                Console.WriteLine("Would you like to order this list by title or author? (y/n)");
                if (Console.ReadLine()?.ToLower() == "y")
                    SortBookList(wishlist);

                DisplayBookList(wishlist);

                string response;
                do
                {
                    Console.Write("Would you like to add a book to your wishlist? (y/n): ");
                    response = (Console.ReadLine() ?? "").Trim().ToLower();
                } while (response != "y" && response != "n");

                if (response == "y")
                    AddBook("wishlist", false);
            }
            else 
                Console.WriteLine("No user is currently selected.");
        }

        static void RentBooksMenu()
        {
            // Get the next user in queue
            currSelect = UserQueueManager.DequeueUser();

            if (Program.currSelect == null)
            {
                Console.WriteLine("No users in the queue.");
                return;
            }

            Console.WriteLine($"Serving: {Program.currSelect.FirstName} {Program.currSelect.LastName}");

            Console.WriteLine("Please enter book information below: ");
            AddBook("rented", false);
            
            UserQueueManager.EnqueueUser(currSelect);
        }


        static void ShowRecommendations(TreeNode node_)
        {
            TreeNode? node = node_;
            
            while (node != null && !node.IsLeaf)
            {
                Console.WriteLine(node.Question + " (y/n)");

                string answer = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (answer == "y" || answer == "yes")
                    node = node.YesChild;
                else if (answer == "n" || answer == "no")
                    node = node.NoChild;
                else
                    Console.WriteLine("Please answer with 'y' or 'n' (or 'yes' / 'no').");
            }

            if (node?.Recommendation != null)
                Console.WriteLine($"\nWe recommend: {node.Recommendation}");

            // Check if they want to get a recommendation based on the previous suggested book
            string response;
            do
            {
                Console.WriteLine("Would you like a recommendation based on a book of your choosing? (y/n):");
                response = (Console.ReadLine() ?? "").Trim().ToLower();
            } while (response != "y" && response != "n");
            
            if (response == "y")
            {
                GraphRecommendation();
            }
        }

        // Helper function to sort/organise the list of books
        static void SortBookList(List<Book> sortList) 
        {
            string sort, order;
            do 
            {
                Console.WriteLine("Sort by: 1) Title 2) Author");
                sort = Console.ReadLine() ?? "";
            } while (sort != "1" && sort != "2");

            if (sort == "1")
                QuickSort.SortBooks(sortList, "Title");
            else if (sort == "2")
                sortList = sortList.OrderBy(b => b.Author).ToList();

            do 
            {
                Console.WriteLine("Order: 1) Ascending 2) Descending");
                order = Console.ReadLine() ?? "";
            } while (order != "1" && order != "2");

            if (order == "2")
                sortList.Reverse();
        }

        // Helper function to add books to the current list
        static void AddBook(string list, bool isRead) 
        {
            Console.Write("Author: ");
            string author = Console.ReadLine() ?? "";
            Console.Write("Title: ");
            string title = Console.ReadLine() ?? "";
            Console.Write("Print Date (type 0 if you do not know): ");
            string printDate = Console.ReadLine() ?? "";
            Console.Write("Genre: ");
            string genre = Console.ReadLine() ?? "";
            Console.Write("Priority (1 is the highest): ");
            int priority = int.Parse(Console.ReadLine() ?? "-1");
            Console.WriteLine("Popularity (1 to 10): ");
            int popularity = int.Parse(Console.ReadLine() ?? "-1");
            var newBook = new Book(author, title, printDate, genre, popularity, -1, priority, isRead);

            if (currSelect != null && currSelect.OwnedBooks.Where(x => x.Author == newBook.Author && x.Title == newBook.Title).Count() < 1)
            {
                if (list == "owned") 
                {
                   currSelect.OwnedBooks.Add(newBook);
                    if (currSelect.FirstName == "John")
                        ListUpdater.AddToBookList(newBook, "ownedBooks_John.txt");
                    else if (currSelect.FirstName == "Jane")
                        ListUpdater.AddToBookList(newBook, "ownedBooks_Jane.txt");
                    Console.WriteLine("Book has been entered.");
                }
                else if (list == "wishlist")
                { 
                    currSelect.Wishlist.Add(newBook);
                    if (currSelect.FirstName == "John")
                        ListUpdater.AddToBookList(newBook, "wishlistBooks_John.txt");
                    else if (currSelect.FirstName == "Jane")
                        ListUpdater.AddToBookList(newBook, "wishlistBooks_Jane.txt");
                    Console.WriteLine("Book has been entered.");
                }
                else if (list == "rented") 
                {
                    if (!currSelect.Rented.Any(b => b.Title == newBook.Title && b.Author == newBook.Author))
                    {
                        currSelect.Rented.Add(newBook);

                        Console.Write("How many days are you renting this book for?: ");
                        if (!int.TryParse(Console.ReadLine(), out int days))
                        {
                            Console.WriteLine("Invalid number of days.");
                            return;
                        }
            
                        // Check membership status and inform user
                        if (currSelect.IsMember)
                            Console.WriteLine("Congratulations, you're a member! You are renting with priority access.");
                        else
                            Console.WriteLine("You're a guest. Renting will be based on availability.");
            
                        bookManager.RentBook(currSelect, days);

                        if (currSelect.FirstName == "John")
                            ListUpdater.AddToBookList(newBook, "rentedBooks_John.txt");
                        else if (currSelect.FirstName == "Jane")
                            ListUpdater.AddToBookList(newBook, "rentedBooks_Jane.txt");

                        Console.WriteLine("Book has been entered.");
                    }
                    else
                        Console.WriteLine("This book is already in the user's rented list.");
                }
            }
            else
                Console.WriteLine("The book is a duplicate.");
        }

        // Helper function to display the list of books
        static void DisplayBookList(List<Book> books)
        {
            string isRead = "no";
            foreach (var book in books) 
            {
                if (book.IsRead) 
                    isRead = "yes";
                else 
                    isRead = "no";
                Console.WriteLine($"☐ {book.Title} by {string.Join(", ", book.Author)} \n\tGenre: {book.Genre} \n\tPopularity: {book.Popularity}\n\tFinished: {isRead}");
            }
        }

        // Helper function to help recommend a book based on graph
        static void GraphRecommendation() 
        {
            string response;

            Console.WriteLine("What book do you want a recommendation based on? ([Title]:[Author]):");
            string[] b = (Console.ReadLine() ?? "").Split(":");

            if (b.Length == 2)
            {
                // Get recommendations based off of the book relationship graph
                var recs = bookRelationships.bookRelationshipGraph.GetEdges().Where(x =>x.From.Data?.Title == b[0] &&  x.From.Data.Author == b[1]).OrderByDescending(x => x.Weight).ToList();
                if (recs.Count > 0)
                {
                    int recId = 0;
                    while (recId != recs.Count)
                    {
                        Console.WriteLine($"☐ {recs[recId].To.Data?.Title} by {string.Join(", ", recs[recId].To.Data?.Author)} \n\tGenre: {recs[recId].To.Data?.Genre} \n\tPopularity: {recs[recId].To.Data?.Popularity}");
                        recId++;
                        
                        if (recId != recs.Count)
                        {
                            do
                            {
                                Console.WriteLine("Would you like another recommendation? (y/n):");
                                response = (Console.ReadLine() ?? "").Trim().ToLower();
                            } while (response != "y" && response != "n");
                            
                            if (response == "n")
                                break;
                        }
                    }
                }
                else
                    Console.WriteLine("No recommendations could be made based on this book.");
            }
        }
        

        // Helper function to build the tree for recommendations
        static TreeNode BuildTree()
        {
            // Root node asking whether the user prefers fiction or non-fiction
            TreeNode root = new TreeNode { Question = "Do you prefer fiction?" };

            // Fiction subtree (3 levels deep)
            root.YesChild = new TreeNode { Question = "Do you like fantasy?" };

            // Fantasy branch (yes -> Harry Potter)
            root.YesChild.YesChild = new TreeNode { Recommendation = "Harry Potter by J.K. Rowling" };

            // Sci-fi branch (yes -> Dune)
            root.YesChild.NoChild = new TreeNode { Question = "Do you like sci-fi?" };
            root.YesChild.NoChild.YesChild = new TreeNode { Recommendation = "Dune by Frank Herbert" };
            
            // Sci-fi branch (no -> The Great Gatsby)
            root.YesChild.NoChild.NoChild = new TreeNode { Recommendation = "1984 by George Orwell" };

            // Non-fiction subtree
            root.NoChild = new TreeNode { Question = "Do you like biographies?" };

            // Biography branch (yes -> Steve Jobs)
            root.NoChild.YesChild = new TreeNode { Recommendation = "Steve Jobs by Walter Isaacson" };

            // Biography branch (no -> Educated)
            root.NoChild.NoChild = new TreeNode { Question = "Do you like history?" };

            // History branch (yes -> Sapiens)
            root.NoChild.NoChild.YesChild = new TreeNode { Recommendation = "Sapiens by Yuval Noah Harari" };

            // History branch (no -> The Wright Brothers)
            root.NoChild.NoChild.NoChild = new TreeNode { Recommendation = "The Wright Brothers by David McCullough" };

            return root;
        }
    }
}