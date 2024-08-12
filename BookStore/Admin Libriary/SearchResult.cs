using System.Collections;

namespace Admin_Libriary
{
    public class SearchResult : IEnumerable<BookInfoDTO>
    {
        private readonly List<BookInfoDTO> Result;
        public BookInfoDTO this[int index]
        {
            get { return Result[index]; }
            set { Result[index] = value; }
        }


        internal SearchResult(List<BookInfoDTO> list)
        {
            Result = new List<BookInfoDTO>(list);
        }

        public List<BookInfoDTO> GetAsList()
        {
            return new List<BookInfoDTO>(Result);
        }

        public IEnumerator<BookInfoDTO> GetEnumerator()
        {
            foreach (var a in Result)
                yield return a;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}