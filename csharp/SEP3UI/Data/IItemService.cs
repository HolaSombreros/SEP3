using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;


namespace SEP3UI.Data {
    public interface IItemService {
        Task<IList<Item>> GetItemsAsync();
    }
}