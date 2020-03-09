using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderFulfilmentService 
{
  public class OrderRepository : IOrderRepository 
  {
    private IEnumerable<OrderEntity> OrderEntities { get; set; } 

    public OrderRepository()
    {
      InitEntities();
    }

    public IEnumerable<OrderEntity> GetOrders() {
      return OrderEntities.ToList();
    }
    
    public void UpdateStatus(int orderId, string status) {
      var orderEntity = OrderEntities.FirstOrDefault( order => order.OrderId == orderId );

      if (orderEntity != default(OrderEntity)) {
        orderEntity.Status = status;
      } 
    }

    private void InitEntities() 
    {
      var filepath = $"{Directory.GetCurrentDirectory()}\\data.json";

      // read JSON directly from a file
      using (StreamReader file = File.OpenText(filepath))
      using (JsonTextReader reader = new JsonTextReader(file))
      {
        var dataJsonObj = (JObject)JToken.ReadFrom(reader);

        var ordersProp = dataJsonObj.Property("orders");

        var ordersValue = (JArray) ordersProp.Value;

        OrderEntities = ordersValue.ToObject<IEnumerable<OrderEntity>>();
      }      
    }
  }
}
