using System;
using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public class OrderEntity
  {
    public int OrderId { get; set; }  
    public string Status { get; set; }  
    public DateTime DateCreated { get; set; }  
    public IEnumerable<OrderItemEntity> Items { get; set; }  
  }
}