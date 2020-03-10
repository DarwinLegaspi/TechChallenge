namespace OrderFulfilmentService
{
  public interface IProductsService
  {
    void TakeAwayStock(int productId, int quantity);
  }
}