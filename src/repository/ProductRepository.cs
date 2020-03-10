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

    private void LoadEntities() 
    {
      var filepath = $"{Directory.GetCurrentDirectory()}\\data.json";

      JsonHelper.GetObjects(filepath, "products", 
        (orders) =>  
          ProductEntities =orders.ToObject<IEnumerable<ProductEntity>>());  
    }
  }
}
