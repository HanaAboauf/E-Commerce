using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.BasketDTOs
{
    public record BasketDTO(string Id, ICollection<BasketItemsDTO> Items)
    {
    }
}
