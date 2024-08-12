using Application_Core;
using Application_Core.Public_Models;

namespace Admin_Libriary
{
    public interface IAdminInterface
    {
        Message TryAddingBook(BookKeyDTO book);
        Message TryRemovingBook(RemoveBookDTO book);
        Message TryUpdatingBookPrice(BookPriceUpdateDTO book);
        SearchResult SearchBook(params SearchCriteria[] searchCriteria);
        List<BookInfoDTO> GetBooksInfo();
    }
}