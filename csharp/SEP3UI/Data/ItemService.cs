using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public class ItemService : IItemService {
        private readonly IRestService restService;
        
        public ItemService(IRestService restService) {
            this.restService = restService;
        }

        public async Task<double> GetAverageRating(int itemId) {
            return await restService.GetAsync<double>($"Items/{itemId}/rating");

        }

        public async Task<IList<Item>> GetItemsAsync(int index, string category, string priceOrder, string ratingOrder, string discountOrder, string statusOrder, string search) {
            IList<Item> items = await restService.GetAsync<IList<Item>>($"items?index={index}&category={category}&priceOrder={priceOrder}&ratingOrder={ratingOrder}&discountOrder={discountOrder}&statusOrder={statusOrder}&search={search}");
            return items;
        }
        
        public async Task<Item> GetItemAsync(int id) {
            return await restService.GetAsync<Item>($"items/{id}");
        }

        public async Task<Book> GetBookAsync(int id) {
            return await restService.GetAsync<Book>($"items/books/{id}");
        }

        public async Task<Book> AddBookAsync(BookModel bookModel) {
            return await restService.PostAsync<BookModel, Book>(bookModel, "items/books");
        }

        public async Task<IList<Category>> GetCategoriesAsync() {
            return await restService.GetAsync <IList<Category>>($"items/categories");
        }

        public async Task DeleteCategoryAsync(int id) {
            await restService.DeleteAsync($"items/categories/{id}");
        }

        public async Task<IList<Genre>> GetGenresAsync() {
            return await restService.GetAsync<IList<Genre>>("items/genres");
        }

        public async Task<Item> AddItemAsync(ItemModel itemModel) {
            return await restService.PostAsync<ItemModel, Item>(itemModel, "items");
        }
        

        public async Task<Item> UpdateItemAsync(int id, ItemModel item) {
            return await restService.PutAsync<ItemModel,Item>(item, $"items/{id}");
        }
        public async Task<Book> UpdateBookAsync(int id, BookModel item) {
            return await restService.PutAsync<BookModel,Book>(item, $"items/books/{id}");
        }
        
        public async Task<Category> AddCategoryAsync(Category category) {
            return await restService.PostAsync<Category, Category>(category, "items/categories");
        }
        
        public async Task<IList<Review>> GetItemReviewsAsync(int index,Item item) {
            return await restService.GetAsync<IList<Review>>($"items/{item.Id}/reviews?index={index}");
        }

        public async Task<Review> AddReviewAsync(Review review) {
            return await restService.PostAsync<Review, Review>(review, $"items/{review.ItemId}/reviews");
        }
        public async Task RemoveReviewAsync(int itemId, int customerId) {
            await restService.DeleteAsync($"items/{itemId}/reviews/{customerId}");
        }
        public async Task<bool> GetReviewAsync(int itemId, int customerId) {
            return await restService.GetAsync<bool>($"items/{itemId}/reviews/{customerId}");
        }

        public async Task<Review> UpdateReviewAsync(Review review) {
            return await restService.PutAsync<Review, Review>(review,$"items/{review.ItemId}/reviews");
        }
        
        public async Task<Item> AddToWishlistAsync(int customerId, Item item) {
            Item item1 = await restService.PutAsync<Item, Item>(item,$"items/{customerId}/wishlist/{item.Id}");
            return item;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(int customerId) {
            IList<Item> wishlist = await restService.GetAsync<List<Item>>($"items/{customerId}/wishlist");
            return wishlist;
        }

        public async Task RemoveWishlistedItem(int customerId, int itemId) {
            await restService.DeleteAsync($"items/{customerId}/wishlist/{itemId}");
        }

        public async Task<Item> AddToShoppingCartAsync(Item item, int customerId) {
            Item added = await restService.PutAsync<Item, Item>(item, $"items/{customerId}/shoppingbasket");
            return added;
        }

        public async Task<IList<Item>> GetShoppingCartAsync(int customerId) {
            return await restService.GetAsync<List<Item>>($"items/{customerId}/shoppingbasket");
        }

        public async Task<Item> UpdateShoppingCartAsync(Item item, int itemId, int customerId) {
            Item updated = await restService.PutAsync<Item, Item>(item, $"items/{customerId}/shoppingbasket/{itemId}");
            return updated;
        }

        public async Task RemoveFromShoppingCartAsync(int itemId, int customerId) {
            await restService.DeleteAsync($"items/{customerId}/shoppingbasket/{itemId}");
        }
    }
}