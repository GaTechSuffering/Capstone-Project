namespace BookRentingApp {
    public static class UserQueueManager
    {
        private static List<UserAccount> userList = new List<UserAccount>();
        private static UserAccountComparer comparer = new UserAccountComparer();

        public static void EnqueueUser(UserAccount user)
        {
            userList.Add(user);
            SortQueue();
        }

        public static void UpdateUser(UserAccount user)
        {
            SortQueue();
        }

        public static UserAccount? DequeueUser()
        {
            if (userList.Count == 0) return null;
            var user = userList[0];
            userList.RemoveAt(0);
            return user;
        }

        private static void SortQueue()
        {
            userList.Sort(comparer);
        }
    }
}