using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IItemClient {
        Task<IList<Item>> GetItemsAsync(int index, string category, string priceOrder, string ratingOrder, string discountOrder, string statusOrder, string search);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IList<Item>> GetItemsByIdAsync(int[] itemIds);
        Task<Item> GetItemBySpecificationsAsync(string name, string description, Category category);
        Task<Book> GetBookBySpecificationsAsync(string isbn);
        Task<IList<Category>> GetCategoriesAsync();
        Task<IList<Genre>> GetGenresAsync();
        Task<Item> AddItemAsync(Item item);
        Task<Book> AddBookAsync(Book book);
        Task<Category> AddCategoryAsync(Category category);
        Task<Item> UpdateItemAsync( Item item);
        Task<Book> UpdateBookAsync( Book book);
        Task<IList<Review>> GetItemReviewsAsync(int index,Item item);
        Task<Review> AddReviewAsync(Review review);
        Task RemoveReviewAsync(int itemId, int customerId);
        Task<Review> GetReviewAsync(int customerId, int itemId);
        Task<Review> UpdateReviewAsync(Review review);
        Task<double> GetAverageRatingAsync(int itemId);
        Task<Item> AddToWishlist(int customerId, int itemId);
        Task RemoveWishlistedItemAsync(Customer customer, Item item);
        Task<Item> AddToShoppingCartAsync(Item item, Customer customer);
        Task<IList<Item>> GetShoppingCartAsync(Customer customer);
        Task<Item> UpdateShoppingCartAsync(Item item, Customer customer);
        Task RemoveFromShoppingCartAsync(Item item, Customer customer);
        Task<IList<Item>> GetCustomerWishlistAsync(Customer customer);


    }
}