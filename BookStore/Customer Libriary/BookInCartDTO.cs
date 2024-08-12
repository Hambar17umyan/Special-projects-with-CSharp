namespace Customer_Libriary
{
    public class BookInCartDTO
    {
        public readonly int ISBN;
        public readonly int Count;
        public BookInCartDTO(int isbn, int count)
        {
            Count = count;
            ISBN = isbn;
        }
    }
}