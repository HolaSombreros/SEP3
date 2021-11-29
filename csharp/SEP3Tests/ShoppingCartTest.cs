using NUnit.Framework;
using SEP3Library.Models;

namespace SEP3Tests {
    [TestFixture]
    public class ShoppingCartTest {
        private ShoppingCart cart;

        [SetUp]
        public void SetUp() {
            cart = new ShoppingCart();
        }

        [Test]
        public void AddItem_WhenCartIsEmpty_SetsItemQuantityToOne() {
            int expected = 1;
            Item item = new Item() {
                Quantity = 5
            };
            
            cart.AddToShoppingCart(item);

            int actualCartSize = cart.Items.Count;
            int actualItemQuantity = cart.Items[0].Quantity;
            Assert.AreEqual(expected, actualCartSize);
            Assert.AreEqual(expected, actualItemQuantity);
        }

        [Test]
        public void AddSameItem_WhenCartHasItem_UpdatesItemQuantity() {
            int expectedCartSize = 1;
            int expectedItemQuantity = 2;
            Item item = new Item() {
                Id = 1
            };

            cart.AddToShoppingCart(item);
            cart.AddToShoppingCart(item);
            
            int actualCartSize = cart.Items.Count;
            Assert.AreEqual(expectedCartSize, actualCartSize);
            
            int actualItemQuantity = cart.Items[0].Quantity;
            Assert.AreEqual(expectedItemQuantity, actualItemQuantity);
        }

        [Test]
        public void AddDifferentItem_WhenCartNotEmpty_IncreasesItemQuantity() {
            int expected = 2;
            Item first = new Item() {
                Id = 1
            };
            Item second = new Item() {
                Id = 2
            };

            cart.AddToShoppingCart(first);
            cart.AddToShoppingCart(second);

            int actual = cart.Items.Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveSameItem_WhenCartHasItemQuantityOne_CartIsEmpty() {
            int expected = 0;
            Item item = new Item() {
                Id = 1
            };

            cart.AddToShoppingCart(item);
            cart.RemoveItemFromShoppingCart(item);

            int actual = cart.Items.Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveSameItem_WhenCartHasItemQuantityTwo_ItemQuantityDecreases() {
            int expected = 1;
            Item item = new Item() {
                Id = 1
            };

            cart.AddToShoppingCart(item);
            cart.AddToShoppingCart(item);
            cart.RemoveItemFromShoppingCart(item);

            int actual = cart.Items[0].Quantity;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddItem_WhenCartHasSameItem_UpdateTotal() {
            decimal expected = 12.24M;
            Item item = new Item() {
                Id = 1,
                Price = 6.12M
            };

            cart.AddToShoppingCart(item);
            cart.AddToShoppingCart(item);

            decimal actual = cart.Total;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddItem_WhenCartHasDifferentItems_UpdateTotal() {
            decimal expected = 19.74M;
            Item first = new Item() {
                Id = 1,
                Price = 7.46M
            };
            Item second = new Item() {
                Id = 2,
                Price = 12.28M
            };

            cart.AddToShoppingCart(first);
            cart.AddToShoppingCart(second);

            decimal actual = cart.Total;
            Assert.AreEqual(expected, actual);
        }
    }
}