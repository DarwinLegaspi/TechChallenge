using Nancy;
using Nancy.ModelBinding;
using System;

namespace OrderFulfilmentService 
{
  public class OrderFulfilmentModule: NancyModule
  {
    public OrderFulfilmentModule(IOrderFulfilmentProvider productOrderProvider)
    {
        // Default endpoint
        Get("/", args => "Welcome to NOMSS");
        
        // order fulfilment endpoint
        Post("/api/v1/warehouse/fulfilment", args => 
        {
          OrderFulfilment receivedData = new OrderFulfilment();

          try 
          {
            receivedData = this.Bind<OrderFulfilment>();

            // TODO: Add endpoint validator

            return productOrderProvider.Process(receivedData.orderIds);
          }
          catch(ItemNotFoundException ex) 
          {
            Console.WriteLine(ex);
            
            var response =  (Response)ex.Message;
            
            response.StatusCode = HttpStatusCode.BadRequest;

            return response; 
          }
          catch(Exception ex) 
          {
            Console.WriteLine($"Exception encountered: {ex.Message}");
            
            throw;
          }
        });
    }
   
  }
}