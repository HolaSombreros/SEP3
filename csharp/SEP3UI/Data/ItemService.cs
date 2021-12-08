﻿using System;
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
        
        public async Task<IList<Item>> GetItemsAsync(int index, string category, string priceOrder, string ratingOrder, string search) {
            IList<Item> items = await restService.GetAsync<IList<Item>>($"items?index={index}&category={category}&priceOrder={priceOrder}&ratingOrder={ratingOrder}&search={search}");
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

        public async Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index) {
            return await restService.GetAsync<IList<Item>>($"items?index={index}&searchName={searchName}");
        }

        public async Task<IList<Category>> GetCategoriesAsync() {
            return await restService.GetAsync <IList<Category>>($"items/categories");
        }

        public async Task<IList<Genre>> GetGenresAsync() {
            return await restService.GetAsync<IList<Genre>>("items/genres");
        }

        public async Task<Item> AddItemAsync(ItemModel itemModel) {
            return await restService.PostAsync<ItemModel, Item>(itemModel, "items");
        }

        // public async Task<IList<Item>> GetItemsByCategoriesAsync(Category category, int index) {
        //     return await restService.GetAsync<IList<Item>>(
        //         $"Items?index={index}&category={category.Name}");
        // }

        public async Task<Item> UpdateItemAsync(int id, ItemModel item) {
            return await restService.PutAsync<ItemModel,Item>(item, $"items/{id}");
        }
        public async Task<Book> UpdateBookAsync(int id, BookModel item) {
            return await restService.PutAsync<BookModel,Book>(item, $"items/books/{id}");
        }
        
        public async Task<Category> AddCategoryAsync(Category category) {
            return await restService.PostAsync<Category, Category>(category, "items/categories");
        }

        // public async Task<IList<Item>> GetItemsByPrice(string orderBy, int index) {
        //     return await restService.GetAsync<IList<Item>>($"Items?index={index}&orderBy={orderBy}");
        // }

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
    }
}