namespace Admin_Libriary
{
    public class BookKeyDTO
    {
        public readonly int ISBN;
        public readonly uint Count;

        public BookKeyDTO(int isbn, uint count)
        {
            ISBN = isbn;
            Count = count;
        }
    }
}