using System.ComponentModel;

namespace SEP3Library.Models {
    public enum ItemStatus {
        [Description("Out of stock")] OutOfStock,
        [Description("In stock")] InStock
    }

}