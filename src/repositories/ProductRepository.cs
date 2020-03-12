using System;
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

    public void UpdateQuantityOnHand(IEnumerable<Tuple<int, int>> productQuantities) 
    {
      var products = ProductEntities.ToDictionary( key => key.ProductId,
                                               product => product); 

      foreach(var productQuantity in productQuantities) 
      {
        products[productQuantity.Item1].QuantityOnHand = productQuantity.Item2;
      }
    }  

    public IEnumerable<ProductEntity> GetProducts() 
    {
      return ProductEntities;
    }

    public IEnumerable<ProductEntity> GetProducts(IEnumerable<int> productIds) 
    {
      var producstNotFound = new List<int>();
      
      var products = new List<ProductEntity>();

      foreach(var productId in productIds) 
      {
        var product = ProductEntities.FirstOrDefault( entity => entity.ProductId == productId );
        
        if (product == default(ProductEntity)) 
        {
          producstNotFound.Add(productId);
        }
        else 
        {
          products.Add(product);
        }
      }

      if (producstNotFound.Any()) 
      {
        throw new ItemNotFoundException(EntityType.Product, $"Product(s) not found: {string.Join(",", producstNotFound)}");
      }

      return products;
    }

    public void Reset() 
    {
      LoadEntities(); 
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
