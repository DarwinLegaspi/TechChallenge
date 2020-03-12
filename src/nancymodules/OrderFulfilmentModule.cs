using Nancy;
using Nancy.ModelBinding;
using System;
using System.Linq;

namespace OrderFulfilmentService 
{
  public class OrderFulfilmentModule: NancyModule
  {
    public OrderFulfilmentModule(IOrderFulfilmentProvider productOrderProvider, IPurchaseOrderService poService)
    {
        // Debug Endpoints
        Get("/", args => "Welcome to NOMSS");

        Get("/api/v1/warehouse/fulfilment/purchaseorders", 
          args => poService.GetPurchaseOrders());

        Get("/api/v1/warehouse/fulfilment/statussummary", 
          args => productOrderProvider.StatusSummary());

        Post("/api/v1/warehouse/fulfilment/reset", args => 
        {
          productOrderProvider.Reset();
          
          poService.Reset();

          return "Input Data Reset.";
        });

        
        // Main order fulfilment endpoint
        Post("/api/v1/warehouse/fulfilment", args => 
        {
          OrderFulfilment receivedData = new OrderFulfilment();

          try 
          {
            receivedData = this.Bind<OrderFulfilment>();

            if (!receivedData.orderIds.Any()) 
            {
              return BadRequestResponse("No Order Ids to process."); 
            }

            return productOrderProvider.Process(receivedData.orderIds);
          }
          catch(ItemNotFoundException ex) 
          {
            Console.WriteLine(ex.Message);

            return BadRequestResponse(ex.Message); 
          }
          catch(Exception ex) 
          {
            Console.WriteLine($"Exception encountered: {ex.Message}");
            
            throw;
          }
        });
    }

    private Response BadRequestResponse(string message)
    {
        var response =  (Response)message;
        
        response.StatusCode = HttpStatusCode.BadRequest;

        return response;           
    }
   
  }
}