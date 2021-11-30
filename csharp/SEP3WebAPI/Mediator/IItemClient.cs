using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IItemClient {
        Task<IList<Item>> GetItemsAsync(int index);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IList<Item>> GetItemsByIdAsync(int[] itemIds);
        Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index);
        Task<Item> GetItemBySpecificationsAsync(string name, string description, Category category);
        Task<Book> GetBookBySpecificationsAsync(string isbn);
        Task<IList<Category>> GetCategoriesAsync();
        Task<IList<Genre>> GetGenresAsync();
        Task<Item> AddItemAsync(Item item);
        Task<IList<Item>> GetItemsByCategoryAsync(string category, int index);
        Task<Book> AddBookAsync(Book book);
        Task<Category> AddCategoryAsync(Category category);
        Task<Item> UpdateItemAsync( Item item);
        Task<Book> UpdateBookAsync( Book book);
    }
}