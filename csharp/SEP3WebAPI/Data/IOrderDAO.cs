﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3WebAPI.Data {
    public interface IOrderDAO {
        Task<Order> CreateOrderAsync(OrderModel orderModel);
        Task<IList<Order>> GetOrdersAsync(int index);
        
    }
}