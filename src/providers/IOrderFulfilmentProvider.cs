using System.Collections.Generic;

namespace OrderFulfilmentService
{
  public interface IOrderFulfilmentProvider
  {
    object Process(IEnumerable<int> orderIds); 
    void Reset();
    object StatusSummary();
  }
}