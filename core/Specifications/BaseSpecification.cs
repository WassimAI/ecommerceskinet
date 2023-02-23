using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        //this will include the actual expression (query)
        public Expression<Func<T, bool>> Criteria {get;}

        //this will include the list of expressions, for example when we want to make include statements
        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

        //this is a function that takes an expression which in turns parse this expression into the includes list
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}