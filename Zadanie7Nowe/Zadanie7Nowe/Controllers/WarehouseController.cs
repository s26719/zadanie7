using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Zadanie7Nowe.DTOs;
using Zadanie7Nowe.Models;
using Zadanie7Nowe.Services;

namespace Zadanie7Nowe.Controllers;
[Route("api/warehouse")]
[ApiController]
public class WarehouseController: ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost("add=product")]
    public async Task<IActionResult> AddProductToWarehouse(Product_WarehouseDTO productWarehouseDto)
    {
        try
        {
            
            //1.1 czy pordukt o id istnieje
            bool productExists = await _warehouseService.ExistProductById(productWarehouseDto);
            if (!productExists)
            {
                return BadRequest("Podany produkt nie istenieje");
            }
            
            //1.2 czy magazyn istnieje
            bool warehouseExists = await _warehouseService.ExistWarehouseById(productWarehouseDto);
            if (!warehouseExists)
            {
                return BadRequest("Podany magazyn nie istnieje");
            }
            /*
            //2 czy zamowienie istnieje

            bool orderExists =
                await _warehouseService.ExistsOrderByIdAmount(productWarehouseDto);
            if (!orderExists)
            {
                return BadRequest("Podane zamowienie nie istnieje");
            }
            // 3 czy zamowienie zostalo zrealizowane
            bool orderFulfilled = await _warehouseService.ChcekIfOrderFullFilled(productWarehouseDto);
            if (!orderFulfilled)
            {
                return BadRequest("To zamowienie zostalo juz zrealizowane");
            }

            // 4 zakutalizowanie zamowienia (Kolumny FulFilledAt)

            int? orderId = await _warehouseService.GetOrderId(productWarehouseDto);

            if (!orderId.HasValue)
            {
                // Jeśli nie istnieje odpowiednie zamówienie, zwróć błąd
                return BadRequest("Nie znaleziono odpowiedniego zamówienia dla podanych danych produktu, ilości i czasu utworzenia.");
            }
            await _warehouseService.UpdateOrderFulfilledAt(productWarehouseDto);
          */  
            // 5 wstawienie rekordu do tabeli product_warehouse
            int producWarehouseId = await _warehouseService.AddProductToWarehouse(productWarehouseDto);
            
            //6 zwrocenie wartosci klucza glownego
            return Ok($"Nowo dodany rekord do magazynu ma identyfikator: {producWarehouseId}");

        }
        
        
        catch (Exception e)
        {
            return StatusCode(500, $"Wystąpił błąd: {e.Message}");
        }
     
    }
}