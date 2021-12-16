using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3WebAPI.Data {
    public interface IItemService {
        Task<IList<Item>> GetItemsAsync(int index, string category, string priceOrder, string ratingOrder,
            string discountOrder, string statusOrder, string search);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IList<Category>> GetCategoriesAsync();
        Task<IList<Genre>> GetGenresAsync();
        Task<Item> CreateItemAsync(ItemModel itemModel);
        Task<Book> CreateBookAsync(BookModel bookModel);
        Task<Category> AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task<Item> UpdateItemAsync(int id, ItemModel item);
        Task<Book> UpdateBookAsync(int id, BookModel book);
        Task<IList<Review>> GetItemReviewsAsync(int index, Item item);
        Task<Review> AddReviewAsync(Review review);
        Task RemoveReviewAsync(int itemId, int customerId);
        Task<Review> GetReviewAsync(int customerId, int itemId);
        Task<Review> UpdateReviewAsync(Review review);
        Task<double> GetAverageReviewAsync(int itemId);
        Task<Item> AddToWishlistAsync(int customerId, int itemId);
        Task RemoveWishlistItemAsync(int customerId, int itemId);
        Task<Item> AddToShoppingCartAsync(Item item, int customerId);
        Task<IList<Item>> GetShoppingCartAsync(int customerId);
        Task<Item> UpdateShoppingCartAsync(Item item, int itemId, int customerId);
        Task RemoveFromShoppingCartAsync(int itemId, int customerId);
        Task<IList<Item>> GetCustomerWishlistAsync(int customerId);
    }
}