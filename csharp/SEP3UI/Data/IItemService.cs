using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public interface IItemService {
        Task<IList<Item>> GetItemsAsync(int index, string category, string priceOrder, string ratingOrder, string discountOrder, string statusOrder, string search);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IList<Genre>> GetGenresAsync();
        Task<Item> AddItemAsync(ItemModel itemModel);
        Task<Book> AddBookAsync(BookModel bookModel);
        Task<Category> AddCategoryAsync(Category category);
        Task<IList<Category>> GetCategoriesAsync();
        Task<Item> UpdateItemAsync(int id, ItemModel item);
        Task<Book> UpdateBookAsync(int id, BookModel item);
        Task<IList<Review>> GetItemReviewsAsync(int index, Item item);
        Task<Review> AddReviewAsync(Review review);
        Task RemoveReviewAsync(int itemId, int customerId);
        Task<bool> GetReviewAsync(int itemId, int customerId);
        Task<Review> UpdateReviewAsync(Review review);
        Task<double> GetAverageRating(int itemId);
        
        Task<Item> AddToWishlistAsync(int customerId, Item item);
        Task<IList<Item>> GetCustomerWishlistAsync(int customerId);
        Task RemoveWishlistedItem(int customerId, int itemId);
        public Task<Item> AddToShoppingCartAsync(Item item, int customerId);
        public Task<IList<Item>> GetShoppingCartAsync(int customerId);
        public Task<Item> UpdateShoppingCartAsync(Item item, int itemId, int customerId);
        public Task RemoveFromShoppingCartAsync(int itemId, int customerId);
    }
}