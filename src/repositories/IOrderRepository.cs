using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public interface IOrderRepository
  {
      IEnumerable<OrderEntity> GetOrders(); 
      IEnumerable<OrderEntity> GetOrders(IEnumerable<int> orderIds); 
      void UpdateStatus(int orderId, string status);
      void Reset();
  }
}