using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Data;

namespace SEP3WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase {
        private IItemService service;

        public ItemsController(IItemService service) {
            this.service = service;
        }

        [HttpGet]
        [Route("{id:int}/rating")]
        public async Task<ActionResult<double>> GetAverageReview([FromRoute] int id) {
            try {
                return await service.GetAverageReviewAsync(id);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IList<Item>>> GetItemsAsync([FromQuery] int index,
            [FromQuery] string? search, [FromQuery] string? category, [FromQuery] string? priceOrder,
            [FromQuery] string? ratingOrder, [FromQuery] string discountOrder, [FromQuery] string statusOrder) {
            try {
                IList<Item> items = await service.GetItemsAsync(index, category, priceOrder, ratingOrder, discountOrder, statusOrder, search);
                return Ok(items);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Item>> GetItemAsync([FromRoute] int id) {
            try {
                Item item = await service.GetItemAsync(id);
                return Ok(item);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<ActionResult<IList<Review>>> GetItemReviewsAsync([FromQuery] int index, [FromRoute] int id) {
            try {
                Item item = await service.GetItemAsync(id);
                if (item == null)
                    return NotFound($"No item found with id {id}");
                IList<Review> reviews = await service.GetItemReviewsAsync(index, item);
                return Ok(reviews);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet]
        [Route("{id:int}/reviews/{customerId:int}")]
        public async Task<ActionResult<bool>> GetReviewAsync([FromRoute] int id, [FromRoute] int customerId) {
            try {
                Review review = await service.GetReviewAsync(customerId, id);
                return Ok(review!=null);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost]
        [Route("{id:int}/reviews")]
        public async Task<ActionResult<Review>> AddReviewAsync([FromRoute] int id, [FromBody] Review review) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (review.Rating == 0 && (review.Comment == null || review.Comment.Length < 0)) {
                return BadRequest("Please input either a rating or a comment");
            }
            
            try {
                Review created = await service.AddReviewAsync(review);
                return Created($"/{created.ItemId}/{created.Customer.Id}", created);
            } catch (InvalidDataException e) {
                return Conflict(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPut]
        [Route("{id:int}/reviews")]
        public async Task<ActionResult<Review>> UpdateReviewAsync([FromRoute] int id, [FromBody] Review review) {
            try {
                if (!ModelState.IsValid)
                    return BadRequest("Please specify a review of proper format");
                Review updated = await service.UpdateReviewAsync(review);
                if (updated == null)
                    return NotFound($"The review of the item {id} does not exist");
                return Ok(updated);
            } catch (NullReferenceException e) {
                return NotFound(e);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}/reviews/{customerId:int}")]
        public async Task<ActionResult> RemoveReviewAsync([FromRoute] int id, [FromRoute] int customerId) {
            try {
                Review review = await service.GetReviewAsync(customerId, id);
                if (review == null)
                    return NotFound("The review does not exist");
                await service.RemoveReviewAsync(id, customerId);
                return Ok();
            } catch (InvalidDataException e) {
                 return Conflict(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("Books/{id:int}")]
        public async Task<ActionResult<Book>> GetBookAsync([FromRoute] int id) {
            try {
                Item item = await service.GetBookAsync(id);
                return Ok(item);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<IList<Category>>> GetCategoriesAsync() {
            try {
                IList<Category> categories = await service.GetCategoriesAsync();
                return Ok(categories);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("genres")]
        public async Task<ActionResult<IList<Genre>>> GetGenresAsync() {
            try {
                IList<Genre> genres = await service.GetGenresAsync();
                return Ok(genres);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("categories")]
        public async Task<ActionResult<Category>> AddCategoryAsync([FromBody] Category category) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try {
                Category created = await service.AddCategoryAsync(category);
                return Created($"/{created.Id}", created);
            } catch (InvalidDataException e) {
                return Conflict(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Item>> AddItemAsync([FromBody] ItemModel itemModel) {
            try {
                Item item = await service.CreateItemAsync(itemModel);
                return Created($"/{item.Id}", item);
            } catch (InvalidDataException e) {
                return BadRequest(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("books")]
        public async Task<ActionResult<Book>> AddBookAsync([FromBody] BookModel itemModel) {
            try {
                Book book = await service.CreateBookAsync(itemModel);
                return Created($"/{book.Id}", book);
            } catch (InvalidDataException e) {
                return BadRequest(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Item>> UpdateItemAsync([FromRoute] int id, [FromBody] ItemModel item) {
            try {
                Item updated = await service.UpdateItemAsync(id, item);
                return Ok(updated);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("books/{id:int}")]
        public async Task<ActionResult<Book>> UpdateBookAsync([FromRoute] int id, [FromBody] BookModel item) {
            try {
                Item updated = await service.UpdateBookAsync(id, item);
                return Ok(updated);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
        
          [HttpPut]
        [Route("{customerId:int}/wishlist/{itemId:int}")]
        public async Task<ActionResult<Item>> AddToWishlist([FromRoute] int customerId, [FromRoute] int itemId) {
            try {
                Item item = await service.AddToWishlistAsync(customerId, itemId);
                return Ok(item);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{customerId:int}/wishlist/{itemId:int}")]
        public async Task<ActionResult> RemoveWishlistedItemAsync([FromRoute] int customerId, [FromRoute] int itemId) {
            try {
                await service.RemoveWishlistItemAsync(customerId, itemId);
                return Ok();
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{customerId:int}/shoppingBasket")]
        public async Task<ActionResult<Item>> AddShoppingCartAsync([FromBody] Item item, [FromRoute] int customerId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                Item item1 = await service.AddToShoppingCartAsync(item, customerId);
                return Ok(item1);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{customerId:int}/shoppingBasket")]
        public async Task<ActionResult> GetShoppingCartAsync([FromRoute] int customerId) {
            try {
                IList<Item> shoppingCart = await service.GetShoppingCartAsync(customerId);
                return Ok(shoppingCart);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{customerId:int}/shoppingBasket/{itemId:int}")]
        public async Task<ActionResult<Item>> EditShoppingCartAsync([FromBody] Item item, [FromRoute] int customerId,
            [FromRoute] int itemId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                Item item1 = await service.UpdateShoppingCartAsync(item, itemId, customerId);
                return Ok(item1);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{customerId:int}/shoppingBasket/{itemId:int}")]
        public async Task<ActionResult> RemoveFromShoppingCartAsync([FromRoute] int itemId, [FromRoute] int customerId) {
            try {
                await service.RemoveFromShoppingCartAsync(itemId, customerId);
                return Ok();
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("categories/{categoryId:int}")]
        public async Task<ActionResult> DeleteCategoryAsync([FromRoute] int categoryId) {
            try {
                await service.DeleteCategoryAsync(categoryId);
                return Ok();
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{customerId:int}/wishlist")]
        public async Task<ActionResult<IList<Item>>> GetCustomerWishlistAsync([FromRoute] int customerId) {
            try {
                IList<Item> wishlist = await service.GetCustomerWishlistAsync(customerId);
                return Ok(wishlist);
            } catch (NullReferenceException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
    
}