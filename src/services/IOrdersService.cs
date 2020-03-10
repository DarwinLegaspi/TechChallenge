using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public interface IOrdersService
  {
    IEnumerable<OrderEntity> GetOrders(); 
    void FulFilOrder(int orderId); 
    void UnFulFilOrder(int orderId);     
  }
}