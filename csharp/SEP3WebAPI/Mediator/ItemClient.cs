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
        
        public async Task<IList<Item>> GetItemsAsync(int index, string category, string priceOrder, string ratingOrder,
            string discountOrder, string statusOrder, string search) {
            ItemMessage req = new ItemMessage() {
                Type = "getAll",
                Index = index,
                PriceOrder = priceOrder,
                RatingOrder = ratingOrder,
                DiscountOrder = discountOrder,
                StatusOrder = statusOrder,
                Items = new List<Item>() {
                    new() {
                    Name = search
                    }
                },
                Categories = new List<Category>() {
                    new() {
                        Name = category
                    }
                }
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<IList<Category>> GetCategoriesAsync() {
            ItemMessage req = new ItemMessage() {
                Type = "getCategories",
            };
            return ((ItemMessage) client.Send(req)).Categories;
        }

        public async Task<IList<Genre>> GetGenresAsync() {
            ItemMessage req = new ItemMessage() {
                Type = "getGenres",
            };
            return ((ItemMessage) client.Send(req)).Genres;
        }

        public async Task<Item> AddItemAsync(Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "addItem",
                Items = new List<Item>() {item}
            };
            return ((ItemMessage) client.Send(req)).Items[0];
        }

        public async Task<Book> AddBookAsync(Book book) {
            ItemMessage req = new ItemMessage() {
                Type = "addBook",
                Book = book
            };
            return ((ItemMessage) client.Send(req)).Book;
        }

        public async Task<Item> GetItemBySpecificationsAsync(string name, string description, Category category) {
            ItemMessage req = new ItemMessage() {
                Type = "getItemBySpecifications",
                Items = new List<Item>() { 
                    new() {
                    Name = name,
                    Description = description,
                    Category = category
                    }
                }
            };
            return ((ItemMessage) client.Send(req)).Items[0];
        }

        public async Task<Book> GetBookBySpecificationsAsync(string isbn) {
            ItemMessage req = new ItemMessage() {
                Type = "getBookBySpecifications",
                Book = new Book() {
                    Isbn = isbn
                }
            };
            return ((ItemMessage) client.Send(req)).Book;
        }

        public async Task<IList<Item>> GetItemsByIdAsync(int[] itemIds) {
            ItemMessage req = new ItemMessage() {
                Type = "getAllById",
                ItemsIds = itemIds
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<Item> GetItemAsync(int id) {
            ItemMessage req = new ItemMessage() {
                Type = "getItem",
                Items = new List<Item>() {
                    new() {
                    Id = id
                    }
                }
            };
           return ((ItemMessage) client.Send(req)).Items[0];
        }

        public async Task<Book> GetBookAsync(int id) {
            ItemMessage req = new ItemMessage() {
                Type = "getBook",
                Items = new List<Item>() {
                    new() {
                        Id = id
                    }
                }
            };
            return ((ItemMessage) client.Send(req)).Book;
        }

        public async Task DeleteCategoryAsync(int id) {
            ItemMessage req = new ItemMessage() {
                Type = "deleteCategory",
                Categories = new List<Category>() {
                    new() {
                        Id = id
                    }
                }
            };
            client.Send(req);
        }

        public async Task<Item> UpdateItemAsync(Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "updateItem",
                Items = new List<Item>(){item}
            };
            return ((ItemMessage) client.Send(req)).Items[0];
        }

        public async Task<Book> UpdateBookAsync(Book book) {
            ItemMessage req = new ItemMessage() {
                Type = "updateBook",
                Book = book
            };
            return ((ItemMessage) client.Send(req)).Book;
        }

        public async Task<Category> AddCategoryAsync(Category category) {
            ItemMessage req = new ItemMessage() {
                Type = "addCategory",
                Categories = new List<Category>() {
                    category
                }
            };
            return ((ItemMessage) client.Send(req)).Categories[0];
        }

        public async Task<IList<Review>> GetItemReviewsAsync(int index, Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "getItemReviews",
                Index = index,
                Items = new List<Item>(){item}
            };
            return ((ItemMessage) client.Send(req)).Reviews;
        }

        public async Task<Review> AddReviewAsync(Review review) {
            ItemMessage req = new ItemMessage() {
                Type = "addReview",
                Reviews = new List<Review>() {
                    review
                }
            };
            return ((ItemMessage) client.Send(req)).Reviews[0];
        }

        public async Task RemoveReviewAsync(int itemId, int customerId) {
            ItemMessage req = new ItemMessage() {
                Type = "removeReview",
                Reviews = new List<Review>() {
                    new() {
                    Customer = new Customer() {
                        Id = customerId
                        },
                    ItemId = itemId
                    }
                }
            };
            client.Send(req);
        }

        public async Task<Review> GetReviewAsync(int customerId, int itemId) {
            ItemMessage req = new ItemMessage() {
                Type = "getReview",
                Reviews = new List<Review>() { 
                    new() {
                    Customer = new Customer() {
                        Id = customerId
                    },
                    ItemId = itemId
                    }
                }
            };
            return ((ItemMessage) client.Send(req)).Reviews[0];
        }

        public async Task<Review> UpdateReviewAsync(Review review) {
            ItemMessage req = new ItemMessage() {
                Type = "updateReview",
                Reviews = new List<Review>() {
                    review
                }
            };
            return ((ItemMessage) client.Send(req)).Reviews[0];
        }

        public async Task<double> GetAverageRatingAsync(int itemId) {
            Message request = new ItemMessage() {
                Type = "getAverageRating",
                Items = new List<Item>() {
                    new() {Id = itemId}
                }
            };
            return ((ItemMessage)client.Send(request)).AverageRating;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "getWishlist",
                Customer = customer
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<Item> AddToWishlist(int customerId, int itemId) {
            ItemMessage req = new ItemMessage() {
                Type = "addWishlist",
                Customer = new Customer() {Id = customerId},
                Items = new List<Item>() {
                    new() {Id = itemId}                
                }
            };
            return ((ItemMessage) client.Send(req)).Items[0];
        }

        public async Task RemoveWishlistItemAsync(Customer customer, Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "removeWishlist",
                Customer = customer,
                Items = new List<Item>() {
                    item
                }
            };
            client.Send(req);
        }

        public async Task<Item> AddToShoppingCartAsync(Item item, Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "addShoppingCart",
                Customer = customer,
                Items = new List<Item>() {
                    item
                }
            };
            return ((ItemMessage) client.Send(req)).Items[0];
        }

        public async Task<IList<Item>> GetShoppingCartAsync(Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "getShoppingCart",
                Customer = customer
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<Item> UpdateShoppingCartAsync(Item item, Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "updateShoppingCart",
                Customer = customer,
                Items = new List<Item>() {
                    item
                }
            };
            return ((ItemMessage) client.Send(req)).Items[0];
        }

        public async Task RemoveFromShoppingCartAsync(Item item, Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "removeShoppingCart",
                Customer = customer,
                Items = new List<Item>() {
                    item
                }
            };
            client.Send(req);
        }
        
        
    }
}