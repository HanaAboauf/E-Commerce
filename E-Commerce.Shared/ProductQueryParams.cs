using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public ProductSortingOptions Sort { get; set; }

        private int _pageIndex = 1;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = (value < 1) ? 1 : value;
        }
        const int DefaultPageSize = 6;
        const int MaxPageSize = 10;
        private int _pageSize = DefaultPageSize;
        public int PageSize
        {
                        get => _pageSize; 
            set
            {
                if(value<0)
                    _pageSize = DefaultPageSize;
                else
                    _pageSize = (value > MaxPageSize) ? MaxPageSize : value;

            }
        }

    }
}
