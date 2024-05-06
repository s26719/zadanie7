using Zadanie7Nowe.DTOs;
using Zadanie7Nowe.Models;

namespace Zadanie7Nowe.Repositories;

public interface IOrderRepository
{
    Task<bool> ExistsOrderByIdAmount(Product_WarehouseDTO productWarehouseDto);
    Task UpdateOrderFulfilledAt(Product_WarehouseDTO productWarehouseDto);
    Task<int?> GetOrderId(Product_WarehouseDTO productWarehouseDto);
}