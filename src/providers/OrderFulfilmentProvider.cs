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

      var orders = Orders.GetOrders(orderIds);

      foreach (var order in orders) {

        var orderEntities = order.Items.Select( item => item);

        var products = Products.GetProducts(
            orderEntities.Select(p => p.ProductId));

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
      return productOrders.Any(po => (po.QuantityOnHand - po.OrderedQuantity) >= 0); 
    }
  }
}
