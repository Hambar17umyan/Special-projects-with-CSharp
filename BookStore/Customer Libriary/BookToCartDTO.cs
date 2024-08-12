namespace Customer_Libriary
{
    public class BookToCartDTO
    {
        public readonly int ISBN;
        public readonly int Count;

        public BookToCartDTO(int isbn, int count)
        {
            ISBN = isbn;
            Count = count;
        }
    }
}