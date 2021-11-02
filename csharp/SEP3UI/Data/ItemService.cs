using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3UI.Data {
    public class ItemService : IItemService {
        
        public Task<Order> MakePurchase(Order order) {
            throw new System.NotImplementedException();
        }

        public Task<IList<Item>> GetItems() {
            throw new System.NotImplementedException();
        }
    }
}