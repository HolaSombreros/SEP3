﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3WebAPI.Data {
    public interface IItemDAO {
        Task<IList<Item>> GetItemsAsync(int index);
        Task<Item> GetItemAsync(int id);
        Task<Book> GetBookAsync(int id);
    }
}