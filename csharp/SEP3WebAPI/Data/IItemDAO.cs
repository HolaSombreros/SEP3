using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3UI.Model;

namespace SEP3WebAPI.Data {
    public interface IItemDAO {
        Task<IList<Item>> GetItemsAsync();
    }
}