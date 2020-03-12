using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public interface IPurchaseOrderService
  {
    void RaisePurchaseOrders(IEnumerable<int> productIds);
    IEnumerable<string> GetPurchaseOrders();
    void Reset();
  }
}