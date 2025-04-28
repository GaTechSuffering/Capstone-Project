namespace BookRentingApp
{
    //implement a class to sort Books using the QuickSort algorithm
    public static class QuickSort
    {
        //implement the function by which the QuickSort algorithm will be called to sort a List of Books
        public static void SortBooks(List<Book> array, string? sortValue)
        {
            Sort(array, 0, array.Count - 1, sortValue);
        }

        //implement the Sort function to sort the List using the QuickSort algorithm
        private static List<Book> Sort(List<Book> array, int lower, int upper, string? sortValue)
        { 
            if (lower < upper) 
            { 
                int p = Partition(array, lower, upper, sortValue); 
                Sort(array, lower, p, sortValue); 
                Sort(array, p + 1, upper, sortValue); 
            } 
            return array; 
        }

        //implement the Partition function to partition the List to be sorted
        private static int Partition(List<Book> array, int lower, int upper, string? sortValue)
        { 
            int i = lower; 
            int j = upper; 
            Book pivot = array[lower];
            do 
            { 
                if (sortValue == null)
                {
                    while (array[i].CompareTo(pivot) < 0) { i++; } 
                    while (array[j].CompareTo(pivot) > 0) { j--; } 
                }
                else
                {
                    while (array[i].CompareTo(pivot, sortValue) < 0) { i++; } 
                    while (array[j].CompareTo(pivot, sortValue) > 0) { j--; } 
                }
                if (i >= j) { break; } 
                Swap(array, i, j); 
            } 
            while (i <= j); 
            return j; 
        }

        //implement the Swap function to swap to items in a list
        private static void Swap(List<Book> array, int first, int second) 
        { 
            Book temp = array[first]; 
            array[first] = array[second]; 
            array[second] = temp; 
        }
    }
}