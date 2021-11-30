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
        
        public async Task<IList<Item>> GetItemsAsync(int index) {
            IList<Item> items = await restService.GetAsync<IList<Item>>($"items?index={index}");
            return items;
        }
        
        public async Task<Item> GetItemAsync(int id) {
            return await restService.GetAsync<Item>($"items/{id}");
        }

        public async Task<Book> GetBookAsync(int id) {
            return await restService.GetAsync<Book>($"items/books/{id}");
        }

        public async Task<IList<Category>> GetCategories() {
            throw new System.NotImplementedException();
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

        public async Task<IList<Item>> GetItemsByCategoriesAsync(Category category, int index) {
            return await restService.GetAsync<IList<Item>>(
                $"Items?index={index}&category={category.Name}");
        }

        public async Task<Category> AddCategoryAsync(Category category) {
            return await restService.PostAsync<Category, Category>(category, "items/categories");
        }
    }
}