using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IItemClient {
        Task<Item> GetItemAsync(int id);
        Task<Item> GetItemBySpecificationsAsync(string name, string description, Category category);
        Task<Book> GetBookBySpecificationsAsync(string isbn);
        Task<IList<Item>> GetItemsByIdAsync(int[] itemIds);
        Task<Book> GetBookAsync(int id);
        Task<IList<Item>> GetItemsAsync(int index);

        Task<IList<Category>> GetCategoriesAsync();
        Task<IList<Genre>> GetGenresAsync();
        Task<Item> AddItemAsync(Item item);
        Task<Book> AddBookAsync(Book book);
       
        Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index);
    }
}