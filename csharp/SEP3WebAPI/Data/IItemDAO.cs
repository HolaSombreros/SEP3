using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Data {
    public interface IItemDAO {
        Task<IList<Item>> GetItemsAsync(int index);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index);
    }
}