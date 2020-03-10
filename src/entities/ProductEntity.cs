namespace OrderFulfilmentService
{
  public class ProductEntity
  {
    public int ProductId { get; set; }  
    public string Description { get; set; }  
    public int QuantityOnHand { get; set; }  
    public int ReorderThreshold { get; set; }  
    public int ReorderAmount { get; set; }  
    public int DeliveryLeadTime { get; set; }  
  }
}