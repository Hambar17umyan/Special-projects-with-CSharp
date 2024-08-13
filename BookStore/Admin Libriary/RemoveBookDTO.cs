namespace Admin_Libriary
{
    public class RemoveBookDTO
    {
        public readonly int Isbn;
        public readonly uint Count;

        public RemoveBookDTO(int isbn, uint count)
        {
            Isbn = isbn;
            Count = count;
        }
    }
}