
namespace OrderFulfilmentService
{
  public class OrderItemEntity 
  {
    public int OrderId { get; set; }  
    public int ProductId { get; set; }  
    public int Quantity { get; set; }  
    public double costPerItem { get; set; }  
  }
}