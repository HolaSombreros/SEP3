using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3WebAPI.Data {
    public interface IItemDAO {
        Task<IList<Item>> GetItemsAsync(int index, string category, string priceOrder, string ratingOrder, string search);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IList<Category>> GetCategoriesAsync();
        Task<IList<Genre>> GetGenresAsync();
        Task<Item> CreateItemAsync(ItemModel itemModel);
        Task<Book> CreateBookAsync(BookModel bookModel);
        Task<Category> AddCategoryAsync(Category category);
        Task<Item> UpdateItemAsync(int id, ItemModel item);
        Task<Book> UpdateBookAsync(int id, BookModel book);
        Task<IList<Review>> GetItemReviewsAsync(int index, Item item);
        Task<Review> AddReviewAsync(Review review);
        Task RemoveReviewAsync(int itemId, int customerId);
        Task<Review> GetReviewAsync(int customerId, int itemId);
        Task<Review> UpdateReviewAsync(Review review);
        Task<double> GetAverageReviewAsync(int itemId);
    }
}