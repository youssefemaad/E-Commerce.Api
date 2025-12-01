using System.Linq.Expressions;
using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    internal static class ProductFilterHelper
    {
        public static Expression<Func<Product, bool>> GetFilterCriteria(ProductQueryParams queryParams)
        {
            return p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                     && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                     && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()));
        }
    }
}
