using Application_Core;
using Application_Core.Public_Models;
using System.Text.Json;

namespace Admin_Libriary
{
    public class AdminService : IAdminInterface
    {
        public List<BookInfoDTO> GetBooksInfo()
        {
            var res = new List<BookInfoDTO>();
            string directoryPath = @"..\..\..\Application Core\bin\Debug\net8.0\";

            string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");
            foreach (string filePath in txtFiles)
            {
                var a = File.ReadAllText(filePath);
                BookModel b = JsonSerializer.Deserialize<BookModel>(a);

                res.Add(new BookInfoDTO(b.Title, b.Description, b.Authors, b.ISBN, b.Category, b.Price, b.StockQuantity));
            }

            return res;
        }

        public SearchResult SearchBook(params SearchCriteria[] searchCriteria)
        {
            List<BookInfoDTO> list = new List<BookInfoDTO>();
            string directoryPath = @"..\..\..\Application Core\bin\Debug\net8.0\";
            string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");
            foreach (string filePath in txtFiles)
            {
                var a = File.ReadAllText(filePath);
                BookModel b = JsonSerializer.Deserialize<BookModel>(a);
                list.Add(new BookInfoDTO(b.Title, b.Description, b.Authors, b.ISBN, b.Category, b.Price, b.StockQuantity));
            }

            foreach (var i in list)
            {
                foreach (var c in searchCriteria)
                {
                    if (c.IsAuthor)
                    {
                        if (!i.Authors.Contains(c.IsAuthorArg))
                        {
                            list.Remove(i);
                        }
                    }
                    if (c.TitleConsistsOf)
                    {
                        if (!i.Title.Contains(c.TitleConsistsOfArg))
                        {
                            list.Remove(i);
                        }
                    }
                    if (c.DescriptionConsistsOf)
                    {
                        if (!i.Description.Contains(c.DescriptionConsistsOfArg))
                        {
                            list.Remove(i);
                        }
                    }
                    if (c.CategoriesAre)
                    {
                        if (!i.Category.HasFlag(c.CategoriesAreArg))
                        {
                            list.Remove(i);
                        }
                    }
                    if (c.PriceIsHigherThan)
                    {
                        if (i.Price < c.PriceIsHigherThanArg)
                        {
                            list.Remove(i);
                        }
                    }
                    if (c.PriceIsLowerThan)
                    {
                        if (i.Price > c.PriceIsHigherThanArg)
                        {
                            list.Remove(i);
                        }
                    }
                }
            }

            foreach (var c in searchCriteria)
            {
                if (c.Sort)
                {
                    if (c.SortArg == SortingCriteria.PriceAtoZ)
                        list = list.OrderBy(x => x.Price).ToList();
                    else if (c.SortArg == SortingCriteria.PriceZtoA)
                        list = list.OrderByDescending(x => x.Price).ToList();
                    else if (c.SortArg == SortingCriteria.TitleAtoZ)
                        list = list.OrderBy(x => x.Title).ToList();
                    else if (c.SortArg == SortingCriteria.PriceZtoA)
                        list = list.OrderByDescending(x => x.Title).ToList();
                    else if (c.SortArg == SortingCriteria.Categories)
                        list = list.OrderBy(x => (int)x.Category).ToList();
                    else
                        list = list.OrderBy(x => x.ISBN).ToList();
                }
            }
            return new SearchResult(list);
        }

        public Message TryAddingBook(BookKeyDTO book)
        {
            throw new NotImplementedException();
        }

        public Message TryRemovingBook(RemoveBookDTO book)
        {
            var isbn = book.Isbn;
            var count = book.Count;
            if (CheckIfItemExists(isbn))
            {
                if (CheckIfThereAreSufficientBooks(isbn, count))
                {
                    string path = @$"..\..\..\Application Core\bin\Debug\net8.0\{isbn}.txt";
                    var a = File.ReadAllText(path);
                    BookModel b = JsonSerializer.Deserialize<BookModel>(a);
                    b.StockQuantity -= count; 
                    if (b.StockQuantity == 0)
                    {
                        File.Delete(path);
                    }
                    else
                        File.WriteAllText(path, JsonSerializer.Serialize(b));

                    return Message.Success;
                }
                else
                {
                    return new Message("Cannot remove book.", $"There aren't sufficient books with the ISBN number {isbn} in our store!");
                }
            }
            else
            {
                return new Message("Cannot remove book.", $"There are no books with the ISBN number {isbn} in our store!");
            }
        }

        private bool CheckIfItemExists(int isbn)
        {
            string directoryPath = @"..\..\..\Application Core\bin\Debug\net8.0\";
            string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");

            foreach (var item in txtFiles)
            {
                var y = item.Trim().Split('.');
                if (y[0] == isbn.ToString())
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckIfThereAreSufficientBooks(int isbn, int count)
        {
            var items = File.ReadLines(@"..\..\..\Application Core\bin\Debug\net8.0\BookStoreBooks.txt");
            foreach (var item in items)
            {
                var y = item.Trim().Split(' ');
                if (y[0] == isbn.ToString())
                {
                    return Convert.ToInt32(y[1]) >= count;
                }
            }
            return false;
        }

        public Message TryUpdatingBookPrice(BookPriceUpdateDTO book)
        {
            throw new NotImplementedException();
        }
    }
}
