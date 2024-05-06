using System.ComponentModel.DataAnnotations;

namespace Zadanie7Nowe.Models;

public class Warehouse
{
    [Required]
    public int IdWarehouse { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Address { get; set; }
}
