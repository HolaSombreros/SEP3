using System.Collections.Generic;
using SEP3Library.Models;

namespace SEP3Library.UIModels {
    public class ReturnItemsModel {
        public int Id { get; set; }
        public IList<Item> Items { get; set; }
    }
}