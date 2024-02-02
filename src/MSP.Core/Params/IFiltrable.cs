using System.Linq.Expressions;
using MSP.Core.Models;

namespace MSP.Core.Params;

public interface IFiltrable<TEntity> where TEntity : Entity
{
    Expression<Func<TEntity, bool>> Filter();
}