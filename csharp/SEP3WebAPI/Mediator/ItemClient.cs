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
        
        public async Task<IList<Item>> GetItemsAsync(int index, string category, string priceOrder, string ratingOrder, string search) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getAll",
                Index = index,
                PriceOrder = priceOrder,
                RatingOrder = ratingOrder,
                Item = new Item() {
                    Name = search
                },
                Categories = new List<Category>()
            };
            req.Categories.Add(new Category() {
                Name = category
            });
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

        public async Task RemoveReviewAsync(int itemId, int customerId) {
            ItemMessage req = new ItemMessage() {
                Type = "removeReview",
                Service = "item",
                Review = new Review() {
                    Customer = new Customer() {
                        Id = customerId
                    },
                    ItemId = itemId
                }
            };
            client.Send(req);
        }

        public async Task<Review> GetReviewAsync(int customerId, int itemId) {
            ItemMessage req = new ItemMessage() {
                Type = "getReview",
                Service = "item",
                Review = new Review() {
                    Customer = new Customer() {
                        Id = customerId
                    },
                    ItemId = itemId
                }
            };
            return ((ItemMessage) client.Send(req)).Review;

        }

        public async Task<Review> UpdateReviewAsync(Review review) {
            ItemMessage req = new ItemMessage() {
                Type = "updateReview",
                Service = "item",
                Review = review
            };
            return ((ItemMessage) client.Send(req)).Review;
        }
    }
}