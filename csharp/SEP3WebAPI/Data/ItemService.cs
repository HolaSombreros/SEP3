using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    
    public class ItemService : IItemService {
        private IItemClient client; 
        
        public ItemService(IItemClient client) {
            this.client = client;
        }
        
        public async Task<IList<Item>> GetItemsAsync(int index, string category, string priceOrder, string ratingOrder, string search) {
            return await client.GetItemsAsync(index, category, priceOrder, ratingOrder, search);
        }

        public async Task<Item> GetItemAsync(int id) {
            return await client.GetItemAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category category) {
            IList<Category> existing = await client.GetCategoriesAsync();
            if (existing.Any(c => c.Name.ToLower().Equals(category.Name.ToLower()))) {
                throw new InvalidDataException("That category name already exists");
            }

            Category created = await client.AddCategoryAsync(category);
            return created;
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
            toUpdate.ImageName = item.ImageName;
            
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
            toUpdate.ImageName = book.ImageName;
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
                ImageName = itemModel.ImageName
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
                ImageName = itemModel.ImageName,
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

        public async Task<IList<Review>> GetItemReviewsAsync(int index,Item item) {
            return await client.GetItemReviewsAsync(index,item);
        }

        public async Task<Review> AddReviewAsync(Review review) {
            return await client.AddReviewAsync(review);
        }

        public async Task RemoveReviewAsync(int itemId, int customerId) {
            await client.RemoveReviewAsync(itemId, customerId);
        }

        public async Task<Review> GetReviewAsync(int customerId, int itemId) {
            return await client.GetReviewAsync(customerId, itemId);
        }

        public async Task<Review> UpdateReviewAsync(Review review) {
            return await client.UpdateReviewAsync(review);
        }
        public async Task<double> GetAverageReviewAsync(int itemId) {
            return await client.GetAverageRatingAsync(itemId);
        }

    }
    
}