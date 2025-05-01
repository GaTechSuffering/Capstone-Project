namespace BookRentingApp {
    public class UserAccountComparer : IComparer<UserAccount>
    {
        public int Compare(UserAccount? x, UserAccount? y)
        {
            if (x == null || y == null) return 0;
    
            if (x.IsMember && !y.IsMember) return -1;
            if (!x.IsMember && y.IsMember) return 1;
    
            return x.MemberPrice.CompareTo(y.MemberPrice);
        }
    }
}