using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Product;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
 

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(CancellationToken ct = default)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var products = await Repo.GetAllAsync(ct);
            var Data = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return Result<IReadOnlyList<ProductDto>>.Ok(Data);
        }
        public async Task<Result<ProductDto>> GetProductAsync(int id, CancellationToken ct = default)
        {
           var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id, ct);
            if (Product == null)
            {
                return Result<ProductDto>.Fail(Error.NotFound("Product not found",$"Product with ID {id} not found"));
            }
           return _mapper.Map<ProductDto>(Product);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
             var types =  await _unitOfWork. GetRepository<ProductType, int>().GetAllAsync(ct);  
            return Result<IReadOnlyList<TypeDto>>.Ok(_mapper.Map<IReadOnlyList<TypeDto>>(types));
        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);
            return Result<IReadOnlyList<BrandDto>>.Ok(_mapper.Map<IReadOnlyList<BrandDto>>(Brands));
        }

        public Task<Result<IReadOnlyList<ProductDto>>> GetAllProducts()
        {
            throw new NotImplementedException();
        } 
    }
}
