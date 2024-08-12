using Application_Core;
using Application_Core.Public_Models;

namespace Customer_Libriary
{
    public interface ICustomerInterface
    {
        Message TryAddingToCart(BookToCartDTO book);
        SearchResult SearchBook(params SearchCriteria[] searchCriteria);
        List<BookInfoDTO> GetBooksInfo();
        Message TryPurchasingBooksOnCart();
        void ClearTheCart();
    }
}
