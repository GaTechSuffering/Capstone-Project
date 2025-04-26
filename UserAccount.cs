using System;

namespace BookRentingApp
{
    public class UserAccount
    {
        // Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsMember { get; set; }
        public int MemberPrice { get; set; }

        // The books that the account has
        public List<Book> OwnedBooks { get; set; }
        public List<Book> Wishlist { get; set; }

        // Constructor
        public UserAccount(string firstName, string lastName, int age, bool isMember, int memberPrice, List<Book> ownedBooks, List<Book> wishList)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            IsMember = isMember;
            MemberPrice = memberPrice;
            OwnedBooks = new List<Book>();
            Wishlist = new List<Book>();
        }

        // Full name helper 
        public string FullName => $"{FirstName} {LastName}";

        // Display user info
        public void DisplayInfo()
        {
            System.Console.WriteLine($"Name: {FullName}");
            System.Console.WriteLine($"Age: {Age}");
            System.Console.WriteLine($"Membership Status: {(IsMember ? "Member" : "Guest")}");
            System.Console.WriteLine($"Membership Cost: {(IsMember ? 0 : 10)}");
            System.Console.WriteLine($"Owned Books: ");
            PrintBookList(OwnedBooks);
            System.Console.WriteLine($"WishListed Books: ");
            PrintBookList(Wishlist);
        }

        public void PrintBookList(List<Book> books)
        {
            foreach (var book in books)
            {
                book.DisplayInfo();
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
