using System;
using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public interface IProductRepository
  {
    IEnumerable<ProductEntity> GetProducts(); 
    IEnumerable<ProductEntity> GetProducts(IEnumerable<int> productIds);
    void UpdateQuantityOnHand(IEnumerable<Tuple<int, int>> productQuantities); 
    void Reset();
  }
}