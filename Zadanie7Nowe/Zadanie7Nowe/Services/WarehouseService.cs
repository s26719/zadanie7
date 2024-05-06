using Zadanie7Nowe.DTOs;
using Zadanie7Nowe.Repositories;

namespace Zadanie7Nowe.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProduct_WarehouseRepository _productWarehouseRepository;

    public WarehouseService(IOrderRepository orderRepository, IProductRepository productRepository, IWarehouseRepository warehouseRepository, IProduct_WarehouseRepository productWarehouseRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _productWarehouseRepository = productWarehouseRepository;
    }

    public async Task<bool> ExistProductById(Product_WarehouseDTO productWarehouseDto)
    {
        return await _productRepository.ExistProductById(productWarehouseDto);
    }

    public async Task<bool> ExistWarehouseById(Product_WarehouseDTO productWarehouseDto)
    {
        return await _warehouseRepository.ExistWarehouseById(productWarehouseDto);
    }

    public async Task<bool> ExistsOrderByIdAmount(Product_WarehouseDTO productWarehouseDto)
    {
        return await _orderRepository.ExistsOrderByIdAmount(productWarehouseDto);
    }

    public async Task<bool> ChcekIfOrderFullFilled(Product_WarehouseDTO productWarehouseDto)
    {
        return await _productWarehouseRepository.ChcekIfOrderFullFilled(productWarehouseDto);
    }

    public async Task UpdateOrderFulfilledAt(Product_WarehouseDTO productWarehouseDto)
    {
        await _orderRepository.UpdateOrderFulfilledAt(productWarehouseDto);
    }

    public async Task<int> AddProductToWarehouse(Product_WarehouseDTO productWarehouseDto)
    {
        return await _productWarehouseRepository.AddProductToWarehouse(productWarehouseDto);
    }

    public async Task<int?> GetOrderId(Product_WarehouseDTO productWarehouseDto)
    {
        return await _orderRepository.GetOrderId(productWarehouseDto);
    }
    
    
}