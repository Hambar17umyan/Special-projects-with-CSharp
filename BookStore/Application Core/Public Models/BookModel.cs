
namespace Application_Core.Public_Models
{

    public class BookModel
    {
        public decimal Price;
        public uint StockQuantity;

        public readonly string Title;
        public readonly string Description;
        public readonly string[] Authors;
        public readonly int ISBN;
        public readonly BookCategory Category;
        public BookModel(string title, string description, string[] authors, int iSBN, BookCategory category, decimal price, uint quantity)
        {
            Title = title;
            Description = description;
            Authors = authors;
            ISBN = iSBN;
            Category = category;
            Price = price;
            StockQuantity = quantity;
        }
    }
}