using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class RestService : IRestService {
        private IClient client;
        
        public RestService() {
            client = new Client();
        }
        
        public async Task<IList<Item>> GetItemsAsync(int index) {
            return await client.GetItemsAsync(index);
        }

        public async Task<Item> GetItemAsync(int id) {
            return await client.GetItemAsync(id);
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            Customer customer = await client.GetCustomerAsync(email, password);
            return customer;
        }

        public async Task<Customer> GetCustomerAsync(int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            return customer;
        }

        public async Task<Customer> AddCustomerAsync(CustomerModel customer) {
            if (customer == null) throw new InvalidDataException("Please provide a customer of the proper format");
            if (!new EmailAddressAttribute().IsValid(customer.Email)) throw new InvalidDataException("Please enter a valid email address");
            Customer c = new Customer() {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = new Address() {
                    Street = customer.Street,
                    Number = customer.Number,
                    City = customer.City,
                    ZipCode = customer.ZipCode
                },
                PhoneNumber = customer.PhoneNumber,
                Password = customer.Password,
                Role = customer.Role 
            };
            return await client.AddCustomerAsync(c);
        }

        public async Task<Customer> UpdateCustomerAsync(int customerId, UpdateCustomerModel customer) {
            Customer updated = await client.GetCustomerAsync(customerId);
            if (updated == null) throw new NullReferenceException($"No such customer found with id: {customerId}");

            updated.Id = customerId;
            updated.FirstName = customer.FirstName;
            updated.LastName = customer.LastName;
            updated.Email = customer.Email;
            updated.Address.Street = customer.Street;
            updated.Address.Number = customer.Number;
            updated.Address.ZipCode = customer.ZipCode;
            updated.Address.City = customer.City;
            updated.PhoneNumber = customer.PhoneNumber;
            updated.Password = customer.Password;

            await client.UpdateCustomerAsync(updated);
            return updated;
        }

        public Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index) {
            return client.GetItemsBySearchAsync(searchName, index);
        }
        
        public async Task<Category> AddCategoryAsync(Category category) {
            IList<Category> existing = await client.GetCategoriesAsync();
            if (existing.Any(c => c.Name.ToLower().Equals(category.Name.ToLower()))) {
                throw new InvalidDataException("That category name already exists");
            }

            Category created = await client.AddCategoryAsync(category);
            return created;
        }

        public async Task<IList<Item>> GetItemsByCategoryAsync(string category, int index) {
            return await client.GetItemsByCategoryAsync(category, index);
        }

        public async Task<Item> UpdateItemAsync(int id, ItemModel item) {
            if (item == null) throw new InvalidDataException("Please provide an item of the proper format");
            Item toUpdate = await client.GetItemAsync(id);
            if (toUpdate == null) throw new NullReferenceException($"No such item found with id: {id}");
            toUpdate.Name = item.Name;
            toUpdate.Description = item.Description;
            toUpdate.Category = item.Category;
            toUpdate.Price = item.Price;
            toUpdate.Quantity = item.Quantity;
            toUpdate.Status = item.Status;
            toUpdate.Discount = item.Discount;
            toUpdate.ImageName = "Item/haha";
            
            await client.UpdateItemAsync(toUpdate);
            return toUpdate;
        }

        public async Task<Book> UpdateBookAsync(int id, BookModel book) {
            if (book == null) throw new InvalidDataException("Please provide an item of the proper format");
            Book toUpdate = await client.GetBookAsync(id);
            if (toUpdate == null) throw new NullReferenceException($"No such book found with id: {id}");
            toUpdate.Name = book.Name;
            toUpdate.Description = book.Description;
            toUpdate.Category = book.Category;
            toUpdate.Price = book.Price;
            toUpdate.Quantity = book.Quantity;
            toUpdate.Status = book.Status;
            toUpdate.Discount = book.Discount;
            toUpdate.ImageName = "Item/haha";
            toUpdate.Authors = book.Authors;
            toUpdate.Genre = book.Genre;
            toUpdate.Isbn = book.Isbn;
            toUpdate.Language = book.Language;
            toUpdate.PublicationDate = new MyDateTime() {
                Year = book.PublicationDate.Year,
                Month = book.PublicationDate.Month,
                Day = book.PublicationDate.Day,
                Hour = book.PublicationDate.Hour,
                Minute = book.PublicationDate.Minute,
                Second = book.PublicationDate.Second
            };
            await client.UpdateBookAsync(toUpdate);
            return toUpdate;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            return await client.GetCustomerWishlistAsync(customer);
        }

        public async Task<Item> AddToWishlistAsync(int customerId, int itemId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item = await client.GetItemAsync(itemId);
            if (item == null) throw new NullReferenceException($"No such item found with id: {itemId}");
            
            return await client.AddToWishlist(customerId, itemId);
        }

        public async Task RemoveWishlistedItemAsync(int customerId, int itemId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item = await client.GetItemAsync(itemId);
            if (item == null) throw new NullReferenceException($"No such item found with id: {itemId}");
            
            await client.RemoveWishlistedItemAsync(customer, item);
        }

        public async Task<Item> AddToShoppingCartAsync(Item item, int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item1 = await client.GetItemAsync(item.Id);
            if (item1 == null) throw new NullReferenceException($"No such item found with id: {item.Id}");

            return await client.AddToShoppingCartAsync(item, customer);
        }

        public async Task<IList<Item>> GetShoppingCartAsync(int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");

            return await client.GetShoppingCartAsync(customer);
        }

        public async Task<Item> UpdateShoppingCartAsync(Item item, int itemId, int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item1 = await client.GetItemAsync(itemId);
            if (item1 == null) throw new NullReferenceException($"No such item found with id: {itemId}");

            return await client.UpdateShoppingCartAsync(item, customer);
        }

        public async Task RemoveFromShoppingCartAsync(int itemId, int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item = await client.GetItemAsync(itemId);
            if (item == null) throw new NullReferenceException($"No such item found with id: {itemId}");

            await client.RemoveFromShoppingCartAsync(item, customer);
        }

        public async Task<Book> GetBookAsync(int id) {
            return await client.GetBookAsync(id);
        }

        public async Task<IList<Category>> GetCategoriesAsync() {
            return await client.GetCategoriesAsync();
        }

        public async Task<IList<Genre>> GetGenresAsync() {
            return await client.GetGenresAsync();
        }

        public async Task<Item> CreateItemAsync(ItemModel itemModel) {
            if (itemModel == null) throw new InvalidDataException("Please specify an item of the proper format!");
            Item item = await client.GetItemBySpecificationsAsync(itemModel.Name, itemModel.Description, itemModel.Category);
            if (item != null)
                throw new InvalidDataException("This item already exists");
            Item i = new Item() {
                Name = itemModel.Name,
                Description = itemModel.Description,
                Category = itemModel.Category,
                Discount = itemModel.Discount,
                Price = itemModel.Price,
                Status = ItemStatus.InStock,
                Quantity = itemModel.Quantity,
                ImageName = "Images/sth"
            };
            return await client.AddItemAsync(i);
        }

        public async Task<Book> CreateBookAsync(BookModel itemModel) {
            if (itemModel == null) throw new InvalidDataException("Please specify a book of the proper format!");
            Book book = await client.GetBookBySpecificationsAsync(itemModel.Isbn);
            if (book != null)
                throw new InvalidDataException("This book already exists, please edit in case of stock refill");
            
            Book b = new Book() {
                Name = itemModel.Name,
                Description = itemModel.Description,
                Category = itemModel.Category,
                Discount = itemModel.Discount,
                Price = itemModel.Price,
                Status = ItemStatus.InStock,
                Quantity = itemModel.Quantity,
                ImageName = "Images/Booklala",
                Isbn = itemModel.Isbn,
                Language = itemModel.Language,
                PublicationDate = new MyDateTime() {
                    Year = itemModel.PublicationDate.Year,
                    Month = itemModel.PublicationDate.Month,
                    Day = itemModel.PublicationDate.Day,
                    Hour = itemModel.PublicationDate.Hour,
                    Minute = itemModel.PublicationDate.Minute,
                    Second = itemModel.PublicationDate.Second
                },
                Authors = itemModel.Authors,
                Genre = itemModel.Genre
            };
            return await client.AddBookAsync(b);
        }

        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            if (orderModel == null) throw new InvalidDataException("Please specify an order of the proper format");
            if (orderModel.Items == null || orderModel.Items.Count < 1) throw new InvalidDataException("Your order must contain at least 1 item");
            if (!new EmailAddressAttribute().IsValid(orderModel.Email)) throw new InvalidDataException("Please enter a valid email address");
            int[] itemIds = new int[orderModel.Items.Count];
            for (int i = 0; i < itemIds.Length; i++) {
                itemIds[i] = orderModel.Items[i].Id;
            }
            
            IList<Item> items = await client.GetItemsByIdAsync(itemIds);
            orderModel.Items = orderModel.Items.OrderBy(o => o.Id).ToList();

            for (int i = 0; i < orderModel.Items.Count; i++) {
                if (orderModel.Items[i].Quantity > items[i].Quantity) {
                    if (items[i].Quantity == 0)
                        throw new InvalidDataException(
                            $"Item {orderModel.Items[i].Name} is out of stock. The stock will be updated later");

                    throw new InvalidDataException(
                        $"Item {orderModel.Items[i].Name} amount exceeds the amount available. Only this amount is available {items[i].Quantity}");
                }
            }

            Order order = new Order() {
                FirstName = orderModel.FirstName,
                LastName = orderModel.LastName,
                Email = orderModel.Email,
                Address = new Address() {
                    Street = orderModel.Street,
                    Number = orderModel.Number,
                    City = orderModel.City,
                    ZipCode = orderModel.ZipCode
                },
                DateTime = new MyDateTime() {
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    Day = DateTime.Now.Day,
                    Hour = DateTime.Now.Hour,
                    Minute = DateTime.Now.Minute,
                    Second = DateTime.Now.Second
                },
                Items = orderModel.Items,
                OrderStatus = OrderStatus.Pending,
                CustomerId = orderModel.CustomerId
            };

            return await client.CreateOrderAsync(order);
        }

        public async Task<IList<Order>> GetOrdersAsync(int index) {
            return await client.GetOrdersAsync(index);
        }

        public async Task<IList<Item>> GetItemsByPriceAsync(string orderBy, int index) {
            return await client.GetItemsByPriceAsync(orderBy, index);
        }
    }
}