using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3WebAPI.Mediator.Messages;

namespace SEP3WebAPI.Mediator {
    public class ItemClient : IItemClient {

        private IClient client;

        public ItemClient(IClient client) {
            this.client = client;
        }
        
        public async Task<IList<Item>> GetItemsAsync(int index) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getAll",
                Index = index
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<IList<Category>> GetCategoriesAsync() {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getCategories"
            };
            return ((ItemMessage) client.Send(req)).Categories;
        }

        public async Task<IList<Genre>> GetGenresAsync() {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getGenres"
            };
            return ((ItemMessage) client.Send(req)).Genres;
        }

        public async Task<Item> AddItemAsync(Item item) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "addItem",
                Item = item
            };
            return ((ItemMessage) client.Send(req)).Item;
        }

        public async Task<Book> AddBookAsync(Book book) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "addBook",
                Book = book
            };
            return ((ItemMessage) client.Send(req)).Book;
        }

        public async Task<Item> GetItemBySpecificationsAsync(string name, string description, Category category) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getItemBySpecifications",
                Item = new Item() {
                    Name = name,
                    Description = description,
                    Category = category
                }
            };
            return ((ItemMessage) client.Send(req)).Item;
        }

        public async Task<Book> GetBookBySpecificationsAsync(string isbn) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getBookBySpecifications",
                Book = new Book() {
                    Isbn = isbn
                }
            };
            return ((ItemMessage) client.Send(req)).Book;
        }

        public async Task<IList<Item>> GetItemsByIdAsync(int[] itemIds) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getAllById",
                ItemsIds = itemIds
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<Item> GetItemAsync(int id) {
            ItemMessage req = new ItemMessage() {
                Type = "get",
                Service = "item",
                Item = new Item() {
                    Id = id
                }
            };
           return ((ItemMessage) client.Send(req)).Item;
        }

        public async Task<Book> GetBookAsync(int id) {
            ItemMessage req = new ItemMessage() {
                Type = "book",
                Service = "item",
                Item = new Item() {
                    Id = id
                }
            };
            return ((ItemMessage) client.Send(req)).Book;
        }
        
        public async Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index) {
            ItemMessage req = new ItemMessage() {
                Type = "searchByName",
                Service = "item",
                Item = new Item {
                    Name = searchName
                },
                Index = index
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<IList<Item>> GetItemsByCategoryAsync(string category, int index) {
            ItemMessage req = new ItemMessage() {
                Type = "getAllByCategory",
                Service = "item",
                Item = new Item() {
                    Name = category
                },
                Index = index
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<Item> UpdateItemAsync(Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "updateItem",
                Service = "item",
                Item = item
            };
            return ((ItemMessage) client.Send(req)).Item;
        }

        public async Task<Book> UpdateBookAsync(Book book) {
            ItemMessage req = new ItemMessage() {
                Type = "updateBook",
                Service = "item",
                Book = book
            };
            return ((ItemMessage) client.Send(req)).Book;
        }

        public async Task<Category> AddCategoryAsync(Category category) {
            ItemMessage req = new ItemMessage() {
                Type = "addCategory",
                Service = "item",
                Categories = new List<Category>()
            };
            req.Categories.Add(category);
            return ((ItemMessage) client.Send(req)).Categories[0];
        }

        public async Task<IList<Item>> GetItemsByPriceAsync(string orderBy, int index) {
            ItemMessage req = new ItemMessage() {
                Type = "getAllByPrice",
                Service = "item",
                OrderBy = orderBy,
                Index = index
            };
            return ((ItemMessage)client.Send(req)).Items;
        }

        public async Task<IList<Review>> GetItemReviewsAsync(int index,Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "getItemReviews",
                Service = "item",
                Index = index,
                Item = item
            };
            return ((ItemMessage) client.Send(req)).Reviews;
        }

        public async Task<Review> AddReviewAsync(Review review) {
            ItemMessage req = new ItemMessage() {
                Type = "addReview",
                Service = "item",
                Reviews = new List<Review>()
            };
            req.Reviews.Add(review);
            return ((ItemMessage) client.Send(req)).Reviews[0];
        }
    }
}