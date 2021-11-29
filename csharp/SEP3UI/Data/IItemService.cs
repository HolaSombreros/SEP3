using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3UI.Data {
    public interface IItemService {
        Task<IList<Item>> GetItemsAsync(int index);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index);
        Task<IList<Category>> GetCategories();
        Task<Category> AddCategoryAsync(Category category);
    }
}