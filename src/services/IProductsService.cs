using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public interface IProductsService
  {
    void ReduceProductQuantities(IEnumerable<ProductOrder> productOrders);
    IEnumerable<ProductEntity> GetProducts(IEnumerable<int> productIds);
    void RaisePurchaseOrderIfNeeded(IEnumerable<ProductOrder> productOrders);
  }
}