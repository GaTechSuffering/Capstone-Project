namespace BookRentingApp
{
    //create a Class for the graph of book relationships
    public class BookRelationshipGraph
    {
        //create a graph of book relationships
        public Graph<Book> bookRelationshipGraph = new Graph<Book>(true, true);

        //create a constructor for the book relationsip graph
        public BookRelationshipGraph()
        {
            //testing hashset
            HashSet<Book> test = new HashSet<Book>();
            test.Add(new Book("a", "a", "1", "a", 0, 0, 0, false));
            test.Add(new Book("b", "b", "2", "a", 0, 0, 0, false));
            test.Add(new Book("a", "a", "1", "a", 0, 0, 0, false));

            // foreach(var x in test)
            // {
            //     Console.WriteLine(x);
            // }

            //testin hashset end

            //create a set of all books with no duplicates from the files and an array of the series
            List<Book> toAdd = BookLoader.LoadBooksFromFile("PopularBooks.txt");
            toAdd.AddRange(BookLoader.LoadBooksFromFile("ownedBooks_John.txt"));
            toAdd.AddRange(BookLoader.LoadBooksFromFile("ownedBooks_Jane.txt"));
            toAdd.AddRange(BookLoader.LoadBooksFromFile("wishlistBooks_John.txt"));
            toAdd.AddRange(BookLoader.LoadBooksFromFile("wishlistBooks_Jane.txt"));
            toAdd.Select(x => new Book(x.Author, x.Title, x.PrintDate, x.Genre, x.Popularity, 1, 1, false)).ToList();
            HashSet<Book> allBooks = new HashSet<Book>();
            List<Book[]>? series = SeriesLoader.LoadSeriesFromFile();

            foreach(var x in toAdd)
            {
                allBooks.Add(x);
            }

            //add all books to the graph
            foreach (Book b in allBooks)
            {
                //Console.WriteLine(b.Title + " " + b.Author + " " + b.PrintDate + " " + b.Genre);
                bookRelationshipGraph.AddNode(b);
            }

            //add edges to the graph with weights calculated based on if the books are in the same series, by the same author, or in the same genre
            foreach (Node<Book> b in bookRelationshipGraph.Nodes)
            {
                foreach (Node<Book> b2 in bookRelationshipGraph.Nodes.Where(x => !(x.Data?.Author == b.Data?.Author && x.Data?.Title == b.Data?.Title) && (x.Data?.Author == b.Data?.Author || x.Data?.Genre == b.Data?.Genre)))
                {
                    int weight = 0;
                    if (series != null)
                    {
                        bool bIsInSeries = false;
                        bool b2IsInSeries = false;
                        foreach (var s in series)
                        {
                            if (s.Where(x => x.Author == b.Data?.Author && x.Title == b.Data.Title).Count() == 1)
                                bIsInSeries = true;
                            if (s.Where(x => x.Author == b2.Data?.Author && x.Title == b2.Data.Title).Count() == 1)
                                b2IsInSeries = true;
                        }
                        if (bIsInSeries && b2IsInSeries)
                            weight += 30;
                    }
                    if(b.Data?.Author == b2.Data?.Author)
                        weight += 20;
                    if (b.Data?.Genre == b2.Data?.Genre)
                        weight += 10;
                    weight += b2.Data?.Popularity ?? 0;
                    bookRelationshipGraph.AddEdge(b, b2, weight);
                }
            }
        }
    }
}