using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3WebAPI.Data {
    public interface IItemDAO {
        Task<IList<Item>> GetItemsAsync(int index);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IList<Category>> GetCategoriesAsync();
        Task<IList<Genre>> GetGenresAsync();
        Task<Item> CreateItemAsync(ItemModel itemModel);
        Task<Book> CreateBookAsync(BookModel bookModel);
        Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index);
        Task<Category> AddCategoryAsync(Category category);
        Task<IList<Item>> GetItemsByCategoryAsync(string category, int index);
        Task<Item> UpdateItemAsync(int id, ItemModel item);
        Task<Book> UpdateBookAsync(int id, BookModel book);
        Task<IList<Item>> GetItemsByPriceAsync(string orderBy, int index);
    }
}