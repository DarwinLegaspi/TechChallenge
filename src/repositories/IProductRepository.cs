namespace OrderFulfilmentService
{
  public interface IProductRepository
  {
    ProductEntity GetProduct(int productId);
    void UpdateQuantityOnHand(int productId, int quantity);
  }
}