namespace Customer_Libriary
{
    public class BookToCartDTO
    {
        public readonly int ISBN;
        public readonly uint Count;

        public BookToCartDTO(int isbn, uint count)
        {
            ISBN = isbn;
            Count = count;
        }
    }
}