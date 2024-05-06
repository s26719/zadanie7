using System.ComponentModel.DataAnnotations;

namespace Zadanie7Nowe.Models;

public class Order
{
    [Required]
    public int IdOrder { get; set; }
    [Required]
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FullfilledAt { get; set; }
}