using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public interface IOrdersService
  {
    IEnumerable<OrderEntity> GetOrders(); 
    IEnumerable<OrderEntity> GetOrders(IEnumerable<int> orderIds); 
    void FulFilOrder(int orderId); 
    void UnFulFilOrder(int orderId);     
  }
}