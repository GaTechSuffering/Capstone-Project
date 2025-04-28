namespace BookRentingApp
{
    public static class ListUpdater
    {
        public static void AddToBookList(Book newBook, string filePath)
        {
            string fileEntry = Environment.NewLine + newBook.Author + ";" + newBook.Title + ";" + newBook.PrintDate + ";" + newBook.Genre + ";";
            fileEntry += newBook.Popularity + ";" + newBook.NumBooks + ";" + newBook.Priority + ";" + newBook.IsRead;

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
            }

            File.AppendAllText(filePath, fileEntry);
        }

        public static void UpdateBookList(Book updateBook, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
            }

            string[] books = File.ReadAllLines(filePath);
            for (int i = 0; i < books.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(books[i])) continue;

                var parts = books[i].Split(';');

                if (parts.Length < 8)
                {
                    Console.WriteLine($"Invalid line format: {books[i]}");
                    continue;
                }

                string author = parts[0].Trim();
                string title = parts[1].Trim();
                string printDate = parts[2].Trim();
                string genre = parts[3].Trim();
                int popularity = int.Parse(parts[4]);
                int numBooks = int.Parse(parts[5]);
                int priority = int.Parse(parts[6]);
                bool isRead = bool.Parse(parts[7]);

                if (author == updateBook.Author && title == updateBook.Title && printDate == updateBook.PrintDate && genre == updateBook.Genre
                && popularity == updateBook.Popularity && numBooks == updateBook.NumBooks && priority == updateBook.Priority && isRead == updateBook.IsRead)
                {
                    if (isRead == false)
                    {
                        books[i] = books[i].Substring(0, books[i].Length-5) + "true";
                    }
                }
            }
            File.WriteAllLines(filePath, books);
        }
    }
}