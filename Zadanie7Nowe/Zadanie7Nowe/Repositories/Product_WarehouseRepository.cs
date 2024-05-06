using System.Data.SqlClient;
using Zadanie7Nowe.DTOs;


namespace Zadanie7Nowe.Repositories;

public class Product_WarehouseRepository : IProduct_WarehouseRepository
{

    private readonly IOrderRepository _orderRepository;
    private readonly string connectionString;

    public Product_WarehouseRepository(IConfiguration configuration, IOrderRepository orderRepository)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        _orderRepository = orderRepository;
    }

    public async Task<bool> ChcekIfOrderFullFilled(Product_WarehouseDTO productWarehouseDto)
    {
        int? orderId = await _orderRepository.GetOrderId(productWarehouseDto);
        if (!orderId.HasValue)
        {
            Console.WriteLine("nie mozna znalezc pasujacego zamowienia");
        }
       await using var con = new SqlConnection(connectionString);
        await con.OpenAsync();
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT count(*) from Product_Warehouse where IdOrder = @Idorder";
        cmd.Parameters.AddWithValue("@IdOrder", orderId.Value);
        var result = (int)await cmd.ExecuteScalarAsync();
        return result > 0;

    }
    
    public async Task<int> AddProductToWarehouse(Product_WarehouseDTO productWarehouseDto)
    {

        int? orderId = await _orderRepository.GetOrderId(productWarehouseDto);
        if (!orderId.HasValue)
        {
            Console.WriteLine("nie mozna znalezc pasujacego zamowienia");
            return 0;
        }
        await using (var con = new SqlConnection(connectionString))
        {
            await con.OpenAsync();

            await using (var cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAT) VALUES(@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt); SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@IdWarehouse", productWarehouseDto.IdWarehouse);
                cmd.Parameters.AddWithValue("@IdProduct", productWarehouseDto.IdProduct);
                cmd.Parameters.AddWithValue("@IdOrder", orderId.Value);
                cmd.Parameters.AddWithValue("@Amount", productWarehouseDto.Amount);

                // Obliczanie ceny na podstawie ilości i ceny produktu
                double price;
                await using (var getPriceCmd = new SqlCommand("SELECT Price FROM Product WHERE IdProduct = @IdProduct", con))
                {
                    getPriceCmd.Parameters.AddWithValue("@IdProduct", productWarehouseDto.IdProduct);
                    price = Convert.ToDouble(await getPriceCmd.ExecuteScalarAsync());
                }
                cmd.Parameters.AddWithValue("@Price", price * productWarehouseDto.Amount);

                cmd.Parameters.AddWithValue("@CreatedAt", productWarehouseDto.CreatedAt);

                // Wykonanie zapytania i zwrócenie identyfikatora nowo dodanego rekordu w tabeli Product_Warehouse
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }
    }
}