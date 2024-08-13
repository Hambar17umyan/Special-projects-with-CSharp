using Admin_Libriary;
using Application_Core.Public_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Admin_Service
{
    internal class Class1
    {
        static void Main(string[] args)
        {
            AdminService service = new AdminService();
            var a = service.TryAddingNewBook(new NewBookKeyDTO("The song of ice and fire", "", new string[] { "Goerge R. Martin" }, 1111, Application_Core.Public_Models.BookCategory.Fantasy | Application_Core.Public_Models.BookCategory.Romance, 100, 5));
            var b = JsonSerializer.Deserialize<BookModel>(File.ReadAllText("1111.txt"));
        }
    }
}
