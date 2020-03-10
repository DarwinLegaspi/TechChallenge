using System;

namespace OrderFulfilmentService
{
  public class ItemNotFoundException : Exception
  {
      public EntityType ItemType { get; private set; }

      public ItemNotFoundException()
      {
      }

      public ItemNotFoundException(EntityType itemType, string message)
          : base(message)
      {
        ItemType = itemType;
      }

      public ItemNotFoundException(EntityType itemType, string message, Exception inner)
          : base(message, inner)
      {
        ItemType = itemType;
      }
  }  
}
