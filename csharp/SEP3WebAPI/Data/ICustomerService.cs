﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3UI.Data;

namespace SEP3WebAPI.Data {
    public interface ICustomerService {
        Task<Customer> GetCustomerAsync(string email, string password);
        Task<Customer> GetCustomerAsync(int customerId);
        Task<Customer> AddCustomerAsync(CustomerModel customer);
        Task<Customer> UpdateCustomerAsync(int customerId, UpdateCustomerModel customer);
        Task<IList<Notification>> GetNotificationsAsync(int customerId, int index);
        Task<Notification> UpdateSeenNotificationAsync(int customerId, int notificationId);
        Task<IList<Customer>> GetCustomersByIndexAsync(int index);
    }
}