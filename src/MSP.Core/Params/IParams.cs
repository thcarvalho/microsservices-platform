using MSP.Core.Models;

namespace MSP.Core.Params;

public interface IParams<TEntity> : IPaginable, IFiltrable<TEntity>, ISortable where TEntity : Entity
{
}