using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3UI.Model;

namespace SEP3UI.Data {
    public interface IItemService {
        Task<Order> MakePurchase(Order order);
        Task<IList<Item>> GetItems();
    }
}