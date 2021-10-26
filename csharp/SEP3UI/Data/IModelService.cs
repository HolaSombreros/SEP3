using SEP3UI.Model;

namespace SEP3UI.Data {
    public interface IModelService : IItemService, IOrderService {
        public ShoppingCart ShoppingCart { get; init; }
    }
}