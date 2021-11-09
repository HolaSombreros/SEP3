using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3UI.Data {
    public class ItemService : IItemService {
        public async Task<IList<Item>> GetItemsAsync() {
            IList<Item> items = await RestService.GetAsync<IList<Item>>("items");
            return items;
        }

        public async Task<Item> GetItemAsync(int id) {
            return await RestService.GetAsync<Item>($"items/{id}");
        }
    }
}