using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) :
                    base(ProductFilterHelper.GetFilterCriteria(queryParams))
        {
            AddProductIncludes();

            switch (queryParams.SortingOptions)
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

            ApplyPagination(queryParams.PageIndex, queryParams.PageSize);
        }

        public ProductWithBrandAndTypeSpecifications(int id) : base(n => n.Id == id)
        {
            AddProductIncludes();
        }

        private void AddProductIncludes()
        {
            AddInclude(n => n.ProductBrand);
            AddInclude(n => n.ProductType);
        }
    }
}
