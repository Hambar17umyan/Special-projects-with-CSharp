using Application_Core.Public_Models;

namespace Admin_Libriary
{
    public class BookInfoDTO
    {
        public decimal Price;
        public int StockQuantity;

        public readonly string Title;
        public readonly string Description;
        public readonly string[] Authors;
        public readonly int ISBN;
        public readonly BookCategory Category;
        public BookInfoDTO(string title, string description, string[] authors, int iSBN, BookCategory category, decimal price, int stockQuantity)
        {
            Title = title;
            Description = description;
            Authors = authors;
            ISBN = iSBN;
            Category = category;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}