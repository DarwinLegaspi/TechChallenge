using System.Collections.Generic;
using System.Linq;

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
      var unfulfilledOrders = new List<int>();     

      // Gets all requested orders. Throws an ItemNotFound exception if one
      // order is not found
      var orders = Orders.GetOrders(orderIds);

      foreach (var order in orders) 
      {
        // Gets all product items within the order
        var orderEntities = order.Items.Select( item => item).ToList();
  
        // Gets the product entities of all product items
        var products = Products.GetProducts(
            orderEntities.Select(p => p.ProductId));

        // Gets a list of Product entity and the product item order
        var productOrders = CreateProductOrders(products, orderEntities);

        // check if all products in the order can be fulfilled
        // if one product order fail the order, and then continue 
        // with the order run
        if (CanFulFilOrder(productOrders)) 
        {
          Products.ReduceProductQuantities(productOrders);
          
          Orders.FulFilOrder(order.OrderId);
          
          Products.RaisePurchaseOrderIfNeeded(productOrders);          
        }
        else 
        {
          Orders.UnFulFilOrder(order.OrderId);
          
          unfulfilledOrders.Add(order.OrderId);
        }
      }    

      return new { unfulfillable = unfulfilledOrders}; 
    }

    public void Reset()
    {
      Orders.Reset();
    
      Products.Reset();
    }

    public object StatusSummary()
    {
      var orders = Orders.GetOrders();

      var products = Products.GetProducts();

      var summaryObject = new 
      {
          Products = products.Select(p => new {
            p.ProductId,
            p.Description,
            p.QuantityOnHand,
            p.ReorderThreshold
          }) , 
          Orders = orders.Select(o => new {
            o.OrderId,
            o.Status
          })
      };

      return summaryObject;
    }    

    private IEnumerable<ProductOrder> CreateProductOrders(IEnumerable<ProductEntity> products, IEnumerable<OrderItemEntity> orderEntities)
    {
      var result = products.Join(orderEntities,
                    product => product.ProductId,
                    orderEntity => orderEntity.ProductId,
                    (p, oe) => new ProductOrder { 
                        OrderId = oe.OrderId,
                        ProductId = p.ProductId,
                        QuantityOnHand = p.QuantityOnHand,
                        OrderedQuantity = oe.Quantity,
                        ReOrderThreshold = p.ReorderThreshold
                      });

      return result;
    }

    private bool CanFulFilOrder(IEnumerable<ProductOrder> productOrders)
    {
      return productOrders.All(po => (po.QuantityOnHand - po.OrderedQuantity) >= 0); 
    }
  }
}
