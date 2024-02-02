using System.Linq.Expressions;
using LinqKit;
using MSP.Core.Models;
using MSP.Core.Params;

namespace MSP.WebAPI.Models;

public class ApiBaseParams<TEntity> : IParams<TEntity> where TEntity : Entity
{
    public int? Skip { get; set; }
    public int? Take { get; set; }
    public string? OrderBy { get; set; }

    public Expression<Func<TEntity, bool>> Filter()
    {
        var predicate = PredicateBuilder.New<TEntity>();
        predicate.And(x => x.Active);
        return predicate;
    }
}