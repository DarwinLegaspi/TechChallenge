using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrderFulfilmentService
{
  public class ProductRepository : IProductRepository
  {
    private IEnumerable<ProductEntity> ProductEntities { get; set; } 

    public ProductRepository()
    {
      LoadEntities();
    }

    // throws ItemNotFoundException
    public void UpdateQuantityOnHand(int productId, int quantity) 
    {
      var product = GetProduct(productId);

      product.QuantityOnHand = quantity;
    }  

    // throws ItemNotFoundException
    public ProductEntity GetProduct(int productId)
    {
      var productEntity = ProductEntities.FirstOrDefault( product => product.ProductId == productId );

      if (productEntity != default(ProductEntity)) {
        return productEntity;
      } 

      throw new ItemNotFoundException(EntityType.Product, $"Product not found: {productId}");
    }    

    private void LoadEntities() 
    {
      var filepath = $"{Directory.GetCurrentDirectory()}\\data.json";

      JsonHelper.GetObjects(filepath, "products", 
        (orders) =>  
          ProductEntities =orders.ToObject<IEnumerable<ProductEntity>>());  
    }
  }
}
