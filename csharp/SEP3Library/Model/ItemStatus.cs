using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace SEP3Library.Model {
    public enum ItemStatus {
        [Description("Out of stock")] OutOfStock,
        [Description("In stock")] InStock
    }

}