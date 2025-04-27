using System;
using System.Collections.Generic;
using System.IO;

namespace BookRentingApp
{
    public class BookLoader
    {
        public static List<Book> LoadBooksFromFile(string filePath)
        {
            var books = new List<Book>();
            
            //make sure the directory is set to the correct folder
            string dir = Directory.GetCurrentDirectory();
            if (dir.EndsWith("\\bin\\Debug\\net8.0"))
            {
                dir = dir.Remove(dir.Length-17);
            }
            Directory.SetCurrentDirectory(dir);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return books;
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');

                if (parts.Length < 8)
                {
                    Console.WriteLine($"Invalid line format: {line}");
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

                var book = new Book(author, title, printDate, genre, popularity, numBooks, priority, isRead);
                books.Add(book);
                //Console.WriteLine($"Loaded: {title}, {printDate}, {genre}, {popularity}, {numBooks}, {priority}, {isRead}");
            }

            return books;
        }
    }
}