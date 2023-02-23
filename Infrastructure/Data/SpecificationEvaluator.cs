using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); //criteria like p => p.id == ID
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }

    /*
    Explaining the flow:
    1. Starts with the HTTP request, case of getting a product with an ID
    2. In the controller, we create an instance of the corresponding specification and parse the parameters (if any)
    3. We are then taken to the constructor, which executes
    4. Then back to controller, we call the corresponding generic repo method while parsing the specification as parameter
    5. The generic repository then calls for the apply specification method with the specification as parameter, which then runs the specification evaluator 
    6. In the evaluator, takes the query (which is the list of entities - for example products) and the specification then it:
        a- checks if the spec is null
        b- it adds a where clause to the query to run it (get product of ID = 3 for example)
        c- it then aggregates thru the includes method (from the base class) and adds the include statements
        d- it returns the query
    7. back to the generic repo, it gets the requested entity(ies) from the DB
    8. It returns back to the controller with the requested data
    */
}