using Nancy;
using Nancy.ModelBinding;
using System;

namespace OrderFulfilmentService {
  public class OrderFulfilmentModule: NancyModule
  {
    public OrderFulfilmentModule()
    {
        Get("/", args => "Root ");
        
        Post("/api/v1/warehouse/fulfilment", args => 
        {
          OrderFulfilment receivedData = new OrderFulfilment();
         try {
            receivedData = this.Bind<OrderFulfilment>();
          }
          catch(Exception ex) {
            Console.WriteLine($"Exception encountered: {ex.Message}");
            throw;
          }

          return new { unfulfillable = receivedData.orderIds}; 
        });
    }
      
  }
}