using System.Linq.Expressions;
using LinqKit;
using MSP.Core.Models;
using MSP.Core.Params;

namespace MSP.WebAPI.Models;

public abstract class BaseQueryParams<TEntity> : IParams<TEntity> where TEntity : Entity
{
    public int? Skip { get; set; }
    public int? Take { get; set; }
    public string? OrderBy { get; set; }
    public string? Search { get; set; }

    public Expression<Func<TEntity, bool>> Filter()
    {
        var predicate = PredicateBuilder.New<TEntity>();
        predicate = ConfigureFilter(predicate);
        predicate = predicate.And(x => x.Active);
        return predicate;
    }

    public virtual Expression<Func<TEntity, bool>> ConfigureFilter(ExpressionStarter<TEntity> predicate)
       => predicate;
}