using System.Data.SqlClient;
using Zadanie7Nowe.DTOs;

namespace Zadanie7Nowe.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly string connectionString;

    public WarehouseRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task<bool> ExistWarehouseById(Product_WarehouseDTO productWarehouseDto)
    {
       await using var con = new SqlConnection(connectionString);
        await con.OpenAsync();
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * from Warehouse WHERE IdWarehouse = @IdWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", productWarehouseDto.IdWarehouse);
        using var reader = await cmd.ExecuteReaderAsync();
        var result = (int)await cmd.ExecuteScalarAsync();
        await reader.CloseAsync();
        return result > 0;
    }
}