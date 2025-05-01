using System;
using System.Collections.Generic;
using System.IO;

namespace BookRentingApp
{
    public class SeriesLoader
    {
        //load the book series from the text file
        public static List<Book[]>? LoadSeriesFromFile()
        {
            string filePath = "bookSeries.txt";
            
            //make sure the directory is set to the correct folder
            string dir = Directory.GetCurrentDirectory();
            if (dir.EndsWith("\\bin\\Debug\\net8.0"))
            {
                dir = dir.Remove(dir.Length-17);
            }
            Directory.SetCurrentDirectory(dir);

            //make sure the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return null;
            }

            //read in all lines from the files
            string[] lines = File.ReadAllLines(filePath);
            List<Book[]> series = new List<Book[]>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                //get each book from a series
                var books = line.Split('/');
                var seriesArray = new Book[books.Length];
                for (int i = 0; i < books.Length; i++)
                {
                    //add the book to the series array
                    var info = books[i].Split(';');

                    if (info.Length < 2)
                    {
                        Console.WriteLine($"Invalid line format: {info}");
                        continue;
                    }

                    string title = info[0].Trim();
                    string author = info[1].Trim();
                    seriesArray[i] = new Book(author, title, "", "", 0, 0, 0, false);
                }
                series.Add(seriesArray);
            }

            return series;
        }
    }
}