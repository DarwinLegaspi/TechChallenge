using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrderFulfilmentService
{
  public class OrderRepository : IOrderRepository 
  {
    private IEnumerable<OrderEntity> OrderEntities { get; set; } 

    public OrderRepository()
    {
      LoadEntities();
    }

    public IEnumerable<OrderEntity> GetOrders() {
      return OrderEntities.ToList();
    }
    
    // throws ItemNotFoundException
    public void UpdateStatus(int orderId, string status) {
      var orderEntity = OrderEntities.FirstOrDefault( order => order.OrderId == orderId );

      if (orderEntity != default(OrderEntity)) 
      {
        orderEntity.Status = status;
      } 
      else 
      {
        throw new ItemNotFoundException(EntityType.Order, $"Order not found: {orderId}");
      } 

      return ;
    }

    private void LoadEntities() 
    {
      var filepath = $"{Directory.GetCurrentDirectory()}\\data.json";

      JsonHelper.GetObjects(filepath, "orders", 
        (orders) =>  
          OrderEntities =orders.ToObject<IEnumerable<OrderEntity>>());  
    }
  }
}
