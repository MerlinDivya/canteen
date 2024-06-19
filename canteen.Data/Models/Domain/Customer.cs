using System.ComponentModel.DataAnnotations;
namespace canteen.Data.Models.Domain;

public class Customer
{
    public int Id { get; set; }
    [Required]
    public string? item_number { get; set; }
    [Required]
    public string? description { get; set; }
    public string? unit { get; set; }
}