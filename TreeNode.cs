namespace BookRentingApp
{
    public class TreeNode
    {
        public string? Question { get; set; }
        public string? Recommendation { get; set; }
        public TreeNode? YesChild { get; set; }
        public TreeNode? NoChild { get; set; }

        public bool IsLeaf => YesChild == null && NoChild == null;
    }
}