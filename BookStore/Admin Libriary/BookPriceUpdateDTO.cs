namespace Admin_Libriary
{
    public class BookPriceUpdateDTO
    {
        public readonly int ISBN;
        public readonly decimal NewPrice;

        public BookPriceUpdateDTO(int isbn, decimal newPrice)
        {
            ISBN = isbn;    
            NewPrice = newPrice;
        }
    }
}