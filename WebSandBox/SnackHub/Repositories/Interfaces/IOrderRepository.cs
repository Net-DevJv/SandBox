using SnackHub.Models;

namespace SnackHub.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(OrderModel orderModel);
    }
}
