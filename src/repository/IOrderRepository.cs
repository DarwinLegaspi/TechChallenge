using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public interface IOrderRepository
  {
      IEnumerable<OrderEntity> GetOrders(); 
      void UpdateStatus(int orderId, string status);
  }
}