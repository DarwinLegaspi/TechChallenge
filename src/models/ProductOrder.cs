
using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public class ProductOrder
  {
    public int OrderId { get; set; }  
    public int ProductId { get; set; }  
    public int OrderedQuantity { get; set; }  
    public int QuantityOnHand { get; set; }  
    public int ReOrderThreshold { get; set; }  
  }
}