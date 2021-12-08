using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SEP3Library.Models;

namespace SEP3Library.UIModels {
    public class ReturnItemsModel {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public IList<Item> Items { get; set; } = new List<Item>();
    }
}