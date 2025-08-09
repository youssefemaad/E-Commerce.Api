using DomainLayer.Contracts;
using DomainLayer.Models;
using System.Linq.Expressions;


namespace Service.Specifications
{
    abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        public void AddInclude(Expression<Func<TEntity, object>> IncludeExpression)
        {
            IncludeExpressions.Add(IncludeExpression);
        }

        #endregion

        #region Pagination

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; set; }

        protected void ApplyPagination(int PageIndex, int PageSize)
        {
            IsPaginated = true;
            Skip = PageSize * (PageIndex - 1);
            Take = PageSize;
        }

        #endregion

        #region OrderBy

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        #endregion

    }
}