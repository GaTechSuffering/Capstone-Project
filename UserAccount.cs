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
            OwnedBooks = ownedBooks;
            Wishlist = wishList;
            Rented = rented;

        }

        // Full name helper 
        public string FullName => $"{FirstName} {LastName}";

        // Display user info
        public void DisplayInfo()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"Name: {FullName}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Membership Status: {(IsMember ? "Member" : "Guest")}");
            Console.WriteLine($"Membership Cost: ${(IsMember ? 0 : 10)}");
            Console.WriteLine($"Owned Books: ");
            PrintBookList(OwnedBooks);
            Console.WriteLine($"WishListed Books: ");
            PrintBookList(Wishlist);
            Console.WriteLine($"Rented Books: ");
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
