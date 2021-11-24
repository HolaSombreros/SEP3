using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IItemClient {
        Task<Item> GetItemAsync(int id);
        Task<IList<Item>> GetItemsByIdAsync(int[] itemIds);
        Task<Book> GetBookAsync(int id);
        Task<IList<Item>> GetItemsAsync(int index);
        Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index);
    }
}