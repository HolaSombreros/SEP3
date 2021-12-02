using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public interface IItemService {
        Task<IList<Item>> GetItemsAsync(int index);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IList<Genre>> GetGenresAsync();
        Task<Item> AddItemAsync(ItemModel itemModel);
        Task<Book> AddBookAsync(BookModel bookModel);
        Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index);
        Task<Category> AddCategoryAsync(Category category);
        Task<IList<Category>> GetCategoriesAsync();
        Task<IList<Item>> GetItemsByCategoriesAsync(Category category, int index);
        Task<Item> UpdateItemAsync(int id, ItemModel item);
        Task<Book> UpdateBookAsync(int id, BookModel item);
        Task<IList<Item>> GetItemsByPrice(string orderBy, int index);
        Task<IList<Review>> GetItemReviewsAsync(int index, Item item);
    }
}