using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3WebAPI.Mediator {
    public interface IItemClient {
        public Task<Item> GetItemAsync(int id);
        public Task<IList<Item>> GetItemsAsync(int index);
        public Task<Book> GetBookAsync(int id);

    }
}