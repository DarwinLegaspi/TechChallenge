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

    public void TakeAwayStock(int productId, int quantity)
    {
      var product = ProductRepository.GetProduct(productId);

      if (quantity <= product.QuantityOnHand) 
      {
        var newQuantity = product.QuantityOnHand - quantity;
        
        ProductRepository.UpdateQuantityOnHand(productId, newQuantity);

        // TODO: Check if needed to raise purchase order. Might not be the best place 
      }
      else {
        var message = 
          $@"Insuficient quantity on hand for productId: {productId}. 
          Quantity on hand = {product.QuantityOnHand} requested quantity = {quantity}";
        
        throw new InsufficientProductQuantityException(message);
      }

      return;
    }

  }
}
