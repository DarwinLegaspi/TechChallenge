using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrderFulfilmentService
{
  public class OrdersService : IOrdersService
  {
    private IOrderRepository OrderRepository { get; }

    public OrdersService(IOrderRepository repository)
    {
      OrderRepository = repository;    
    }

    public IEnumerable<OrderEntity> GetOrders() 
    {
      return OrderRepository.GetOrders();
    }

    public void FulFilOrder(int orderId) 
    {
      OrderRepository.UpdateStatus(orderId, "FulFilled");
    }

    public void UnFulFilOrder(int orderId) 
    {
      OrderRepository.UpdateStatus(orderId, "Error: Unfulfillable");
    }

  }
}
