using Zadanie7Nowe.DTOs;

namespace Zadanie7Nowe.Repositories;

public interface IWarehouseRepository
{
    Task<bool> ExistWarehouseById(Product_WarehouseDTO productWarehouseDto);
}