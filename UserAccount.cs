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
        public List<Book> Rented { get; set; }

        // Constructor
        public UserAccount(string firstName, string lastName, int age, bool isMember, int memberPrice, List<Book> ownedBooks, List<Book> wishList, List<Book> rented)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            IsMember = isMember;
            MemberPrice = memberPrice;
            OwnedBooks = new List<Book>();
            Wishlist = new List<Book>();
            Rented = new List<Book>();
        }

        // Full name helper 
        public string FullName => $"{FirstName} {LastName}";

        // Display user info
        public void DisplayInfo()
        {
            Console.WriteLine(new string('-', 40));
            System.Console.WriteLine($"Name: {FullName}");
            System.Console.WriteLine($"Age: {Age}");
            System.Console.WriteLine($"Membership Status: {(IsMember ? "Member" : "Guest")}");
            System.Console.WriteLine($"Membership Cost: ${(IsMember ? 0 : 10)}");
            System.Console.WriteLine($"Owned Books: ");
            PrintBookList(OwnedBooks);
            System.Console.WriteLine($"WishListed Books: ");
            PrintBookList(Wishlist);
            System.Console.WriteLine($"Rented Books: ");
            PrintBookList(Rented);
        }

        public void PrintBookList(List<Book> books)
        {
            foreach (var book in books)
            {
                book.DisplayInfo();
                Console.WriteLine(new string('-', 40));
            }
        }

        public override string ToString()
        {
            string member = "no";
            if (IsMember) 
                member = "yes";

            return $"Name: {FirstName} {LastName}, Age: {Age}, " +
                $"Member: {member}, Member Price: ${MemberPrice}, " +
                $"Owned Books: {OwnedBooks.Count}, Wishlist: {Wishlist.Count}, Rented: {Rented.Count}";
        }
    }
}
