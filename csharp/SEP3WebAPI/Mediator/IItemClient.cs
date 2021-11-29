using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IItemClient {
        Task<Item> GetItemAsync(int id);
        Task<IList<Item>> GetItemsAsync(int index);
        Task<Book> GetBookAsync(int id);
        Task<IList<Item>> GetItemsByIdAsync(int[] itemIds);
        Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index);
        Task<Item> GetItemBySpecificationsAsync(string name, string description, Category category);
        Task<IList<Category>> GetCategoriesAsync();
        Task<Item> AddItemAsync(Item item);
        Task<IList<Item>> GetItemsByCategoryAsync(Category category, int index);
    }
}