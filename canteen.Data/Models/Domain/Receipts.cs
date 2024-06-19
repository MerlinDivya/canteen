using System;
using System.ComponentModel.DataAnnotations;

namespace canteen.Data.Models.Domain
{
    public class Receipts
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item Number is required.")]
        public string item_number { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string description { get; set; }

        [Required(ErrorMessage = "Receipt Date is required.")]
        [DataType(DataType.Date)]
        public DateTime rect_date { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0.")]
        public decimal rate { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public decimal quantity { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        public string unit { get; set; }
    }
}
