using System.Collections.Generic;
using System.Linq;

namespace OrderFulfilmentService
{
  public class PurchaseOrderService : IPurchaseOrderService
  {
    public HashSet<int> ProductIds { get; set; }  // would only accept unique value, no duplicates

    public PurchaseOrderService()
    {
      ProductIds = new HashSet<int>();  
    }
    public void RaisePurchaseOrders(IEnumerable<int> productIds) 
    {
      foreach(var productId in productIds) 
      {
        ProductIds.Add(productId);
      }
    } 

    public IEnumerable<string> GetPurchaseOrders()
    {
      return ProductIds.Select(
        pi => $"Purchase Order raised for product Id: {pi}");

    }
  }
}
