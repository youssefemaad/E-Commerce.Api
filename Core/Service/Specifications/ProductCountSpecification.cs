using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
            : base(ProductFilterHelper.GetFilterCriteria(queryParams))
        {
        }
    }
}
