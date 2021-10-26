using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library;

namespace SEP3WebAPI.Data {
    public interface IItemService {
        Task<IList<Item>> GetItemsAsync();
    }
}