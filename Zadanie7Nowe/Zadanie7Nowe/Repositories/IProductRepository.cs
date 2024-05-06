using Zadanie7Nowe.DTOs;

namespace Zadanie7Nowe.Repositories;

public interface IProductRepository
{
    Task<bool> ExistProductById(Product_WarehouseDTO productWarehouseDto);
}