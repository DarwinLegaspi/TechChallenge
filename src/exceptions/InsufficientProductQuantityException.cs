using System;

namespace OrderFulfilmentService
{
  public class InsufficientProductQuantityException : Exception
  {
      public InsufficientProductQuantityException()
      {
      }

      public InsufficientProductQuantityException(string message)
          : base(message)
      {
      }

      public InsufficientProductQuantityException(string message, Exception inner)
          : base(message, inner)
      {
      }
  }
}
