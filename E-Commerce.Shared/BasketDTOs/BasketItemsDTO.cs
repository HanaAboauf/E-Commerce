using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.BasketDTOs
{
    public record BasketItemsDTO(
        int Id, 
        string ProductName,
        string PictureUrl,
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        decimal Price,
        [Range(1, 100, ErrorMessage = "Quantity must be at least 1.")]
        int Quantity)
    {
    }
}
