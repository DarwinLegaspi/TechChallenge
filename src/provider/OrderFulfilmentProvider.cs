using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public class OrderFulfilmentProvider : IOrderFulfilmentProvider 
  {
    public IOrdersService Orders { get; }
    public IProductsService Products { get; }    
    
    public OrderFulfilmentProvider(IOrdersService ordersService, IProductsService productsService)
    {
      Orders = ordersService;

      Products = productsService;
    }

    public object Process(IEnumerable<int> orderIds) 
    {
      // validate Orders
          // validate Orders should validate if product in order still exists
          // will need to return a validation exception if an order is not valid 
          // or a product order is not valid.

      // Get Orders(orderIds)
         // what do you do if order or product is  not found?
         // maybe bad request?

          // major question?  An Order can contain multiple product order,
          // If item order is unfulfilled, should you not process the order 
          // and mark the order as unfulfilled?

      // For each order
         // var productOrders = order.GetProducts(order.itemOrders) 
         
         // var canFullFilOrders = product.CanSupplyProducts([{productId, quantity}])

         // if canFullFilOrders
         //   product.ReduceProductQuantities([{productId, quantity}])
         //   order.fullfilOrder
         //   product.RaisePurchaseOrderIfNeeded
         // else
              // order.UnfullfilOrder
              // Add to unfulfilOrders
         // end     
      // End for each order


      return new { unfulfillable = orderIds}; 
    }
  }
}
