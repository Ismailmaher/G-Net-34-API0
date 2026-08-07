using E_Commerce.API.Attributes;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Product;
using E_Commerce.Domain.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productservice) : APIBaseController
    {
        #region Get ALL Product
        [RedisCashe(100)]
        [HttpGet]
        [ProducesResponseType(typeof(ProductDto[]), statusCode: StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams, CancellationToken ct)
        {
            var Product = await productservice.GetAllProductsAsync(queryParams, ct);
            return ToActionResult(Product);
        }
        #endregion

        #region Get Product
        [HttpGet("{id:int}") ]
        [ProducesResponseType(typeof(ProductDto[]), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id, CancellationToken ct)
        {
            var Product = await productservice.GetProductAsync(id, ct);
            return ToActionResult(Product);
        }
        #endregion
        #region Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            var Brands = await productservice.GetAllBrandsAsync(ct);
            return ToActionResult(Brands);
        }
        #endregion
        #region Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var Types = await productservice.GetAllTypesAsync(ct);
            return ToActionResult(Types);
        }
        #endregion
    }

}
