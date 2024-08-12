using Application_Core;
using Application_Core.Public_Models;
using System.Linq;
using System.Text.Json;

namespace Customer_Libriary
{
    public class CustomerService : Service, ICustomerInterface
    {
        public ShoppingCart ShoppingCart { get; }

        public CustomerService()
        {
            ShoppingCart = new ShoppingCart();
        }

        public List<BookInfoDTO> GetBooksInfo()
        {
            var res = new List<BookInfoDTO>();
            string directoryPath = @"..\..\..\Application Core\bin\Debug\net8.0\";

            string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");
            foreach (string filePath in txtFiles)
            {
                var a = File.ReadAllText(filePath);
                BookModel b = JsonSerializer.Deserialize<BookModel>(a);

                res.Add(new BookInfoDTO(b.Title, b.Description, b.Authors, b.ISBN, b.Category, b.Price));
            }

            return res;
        }

        public SearchResult SearchBook(params SearchCriteria[] searchCriterias)
        {
            List<BookInfoDTO> list = new List<BookInfoDTO>();
            string directoryPath = @"..\..\..\Application Core\bin\Debug\net8.0\";
            string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");
            foreach (string filePath in txtFiles)
            {
                var a = File.ReadAllText(filePath);
                BookModel b = JsonSerializer.Deserialize<BookModel>(a);
                list.Add(new BookInfoDTO(b.Title, b.Description, b.Authors, b.ISBN, b.Category, b.Price));
            }

            foreach (var i in list)
            {
                foreach (var c in searchCriterias)
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

            foreach(var c in searchCriterias)
            {
                if(c.Sort)
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

        public Message TryAddingToCart(BookToCartDTO book)
        {
            return ShoppingCart.TryAddingToCart(book);
        }

        private void RemoveBooks()
        {
            foreach (var i in ShoppingCart)
            {
                string path = @$"..\..\..\Application Core\bin\Debug\net8.0\{i.isbn}.txt";
                var a = File.ReadAllText(path);
                BookModel b = JsonSerializer.Deserialize<BookModel>(a);
                b.StockQuantity -= i.count;
                if(b.StockQuantity == 0)
                {
                    File.Delete(path);
                }
                else
                File.WriteAllText(path, JsonSerializer.Serialize(b));
            }
        }
        public Message TryPurchasingBooksOnCart()
        {
            var e = ShoppingCart.CheckIfAllIsOk();
            if (e != Message.Success)
            {
                return e;
            }
            RemoveBooks();
            ClearTheCart();
            return Message.Success;
        }
        public void ClearTheCart()
        {
            ShoppingCart.ClearTheCart();
        }
    }
}
