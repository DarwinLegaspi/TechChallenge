using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrderFulfilmentService
{
  public class ProductsService : IProductsService
  {
    private IProductRepository ProductRepository { get; }
    
    public ProductsService(IProductRepository repository)
    {
      ProductRepository = repository;    
    }
    public IEnumerable<ProductEntity> GetProducts(IEnumerable<int> productIds) 
    {
       return ProductRepository.GetProducts(productIds);
    } 

    public void ReduceProductQuantities(IEnumerable<ProductOrder> productOrders)
    {
      var productQuanties = productOrders.Select( po => 
        new Tuple<int, int>(po.ProductId, po.QuantityOnHand - po.OrderedQuantity)); // productId, newQuantityOnHand

      ProductRepository.UpdateQuantityOnHand(productQuanties);
    }

    public void RaisePurchaseOrderIfNeeded(IEnumerable<ProductOrder> productOrders)
    {
      var productsToReStock = productOrders.Where(
        po => (po.QuantityOnHand - po.OrderedQuantity) <= po.ReOrderThreshold );

      //TODO call service to restock product  
    }
  }
}
