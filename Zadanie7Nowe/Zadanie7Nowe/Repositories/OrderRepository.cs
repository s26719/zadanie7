using System.Data.SqlClient;
using Zadanie7Nowe.DTOs;
using Zadanie7Nowe.Models;

namespace Zadanie7Nowe.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task<bool> ExistsOrderByIdAmount(Product_WarehouseDTO productWarehouseDto)
    {
        using var con = new SqlConnection(connectionString);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            "Select * from [Order] where IdProduct = @IdProduct and Amount = @Amount and CreatedAt < @CreatedAt";
        cmd.Parameters.AddWithValue("@IdProduct", productWarehouseDto.IdProduct);
        cmd.Parameters.AddWithValue("Amount", productWarehouseDto.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", productWarehouseDto.CreatedAt);
        var result = (int)await cmd.ExecuteScalarAsync();
        return result > 0;
    }
    public async Task UpdateOrderFulfilledAt(Product_WarehouseDTO productWarehouseDto)
    {
        int? orderId = await GetOrderId(productWarehouseDto);
        if (!orderId.HasValue)
        {
            Console.WriteLine("nie udalo sie znalezc pasujacego zamowienia");
        }
        await using (var con = new SqlConnection(connectionString))
        {
            await con.OpenAsync();

            await using (var cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = @"UPDATE [Order] 
                                SET FulfilledAt = @FullFilledAt 
                                WHERE IdOrder = @OrderId";
                cmd.Parameters.AddWithValue("@OrderId", orderId.Value);
                cmd.Parameters.AddWithValue("@FullFilledAt", DateTime.UtcNow);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<int?> GetOrderId(Product_WarehouseDTO productWarehouseDto)
    {
        await using (var con = new SqlConnection(connectionString))
        {
            await con.OpenAsync();

            await using (var cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT IdOrder FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreatedAt";
                cmd.Parameters.AddWithValue("@IdProduct", productWarehouseDto.IdProduct);
                cmd.Parameters.AddWithValue("@Amount", productWarehouseDto.Amount);
                cmd.Parameters.AddWithValue("@CreatedAt", productWarehouseDto.CreatedAt);

                var orderId = await cmd.ExecuteScalarAsync();
                if (orderId != null && orderId != DBNull.Value)
                {
                    return Convert.ToInt32(orderId);
                }
                else
                {
                    return null; // Brak pasującego zamówienia
                }
            }
        }
    }
}