namespace Admin_Libriary
{
    public class RemoveBookDTO
    {
        public readonly int Isbn;
        public readonly int Count;

        public RemoveBookDTO(int isbn, int count)
        {
            Isbn = isbn;
            Count = count;
        }
    }
}