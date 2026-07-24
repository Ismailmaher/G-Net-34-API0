using E_Commerce.Application.Common;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    public class ProductWithTypeAndTypeSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithTypeAndTypeSpecifications(ProductQueryParams queryParams) : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value) && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value) && (string.IsNullOrEmpty(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue)))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(P => P.ProductType);
        }
        public ProductWithTypeAndTypeSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
