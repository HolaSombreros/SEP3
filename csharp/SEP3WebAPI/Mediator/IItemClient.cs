﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3WebAPI.Mediator {
    public interface IItemClient {

        Task<Item> GetItemAsync(int id);
        Task<IList<Item>> GetItemsByIdAsync(int[] itemsId);
        Task<Book> GetBookAsync(int id);
       Task<IList<Item>> GetItemsAsync(int index);


    }
}