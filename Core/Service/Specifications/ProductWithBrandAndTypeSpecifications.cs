using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) :
                    base(P =>(!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId) && (string.IsNullOrWhiteSpace(queryParams.Search) || P.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            AddInclude(n => n.ProductBrand);
            AddInclude(n => n.ProductType);

            switch (queryParams.sort)
            {
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(n => n.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(n => n.Price);
                    break;
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(n => n.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(n => n.Name);
                    break;
                default:
                    AddOrderBy(n => n.Id);
                    break;
            }

            ApplyPagination(queryParams.PageNumber, queryParams.PageSize);
        }

        public ProductWithBrandAndTypeSpecifications(int id) : base(n => n.Id == id)
        {
            AddInclude(n => n.ProductBrand);
            AddInclude(n => n.ProductType);
        }
    }
}
