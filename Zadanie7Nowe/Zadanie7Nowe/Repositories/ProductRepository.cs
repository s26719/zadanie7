using System.Data.SqlClient;
using Zadanie7Nowe.DTOs;
using Zadanie7Nowe.Models;

namespace Zadanie7Nowe.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> ExistProductById(Product_WarehouseDTO productWarehouseDto)
    {
       await using var con = new SqlConnection(connectionString);
        await con.OpenAsync();
       await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * from Product WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", productWarehouseDto.IdProduct);
        using var reader = await cmd.ExecuteReaderAsync();
        var result = (int)await cmd.ExecuteScalarAsync();
        await reader.CloseAsync();
        return result > 0;
    }
}