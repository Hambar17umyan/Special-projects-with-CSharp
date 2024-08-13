namespace Customer_Libriary
{
    public class BookInCartDTO
    {
        public readonly int ISBN;
        public readonly uint Count;
        public BookInCartDTO(int isbn, uint count)
        {
            Count = count;
            ISBN = isbn;
        }
    }
}