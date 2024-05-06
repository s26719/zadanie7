using Zadanie7Nowe.DTOs;

namespace Zadanie7Nowe.Services;

public interface IWarehouseService
{
    Task<bool> ExistProductById(Product_WarehouseDTO productWarehouseDto);
    Task<bool> ExistWarehouseById(Product_WarehouseDTO productWarehouseDto);
    Task<bool> ExistsOrderByIdAmount(Product_WarehouseDTO productWarehouseDto);
    Task<bool> ChcekIfOrderFullFilled(Product_WarehouseDTO productWarehouseDto);
    Task UpdateOrderFulfilledAt(Product_WarehouseDTO productWarehouseDto);
    Task<int> AddProductToWarehouse(Product_WarehouseDTO productWarehouseDto);

    Task<int?> GetOrderId(Product_WarehouseDTO productWarehouseDto);

}