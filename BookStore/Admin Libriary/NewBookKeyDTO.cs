using Application_Core.Public_Models;

namespace Admin_Libriary
{
    public class NewBookKeyDTO
    {
        public readonly decimal Price;
        public readonly uint StockQuantity;

        public readonly string Title;
        public readonly string Description;
        public readonly string[] Authors;
        public readonly int ISBN;
        public readonly BookCategory Category;
        public NewBookKeyDTO(string title, string description, string[] authors, int iSBN, BookCategory category, decimal price, uint quantity)
        {
            Title = title;
            Description = description;
            Authors = authors;
            ISBN = iSBN;
            Category = category;
            Price = price;
            StockQuantity = quantity;
        }

        public NewBookKeyDTO(BookModel book) : this(book.Title, book.Description, book.Authors, book.ISBN, book.Category, book.Price, book.StockQuantity) { }
    }
}