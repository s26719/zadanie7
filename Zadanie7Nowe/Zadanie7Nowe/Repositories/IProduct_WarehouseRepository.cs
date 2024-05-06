using Zadanie7Nowe.DTOs;

namespace Zadanie7Nowe.Repositories;

public interface IProduct_WarehouseRepository
{
    Task<bool> ChcekIfOrderFullFilled(Product_WarehouseDTO productWarehouseDto);
    Task<int> AddProductToWarehouse(Product_WarehouseDTO productWarehouseDto);
}