using System.Linq.Expressions;
using LinqKit;
using MSP.Clients.API.Models;
using MSP.WebAPI.Models;

namespace MSP.Clients.API.QueryParams;

public class ClientQueryParams : BaseQueryParams<Client>
{
    public override Expression<Func<Client, bool>> ConfigureFilter(ExpressionStarter<Client> predicate)
    {
        if (!string.IsNullOrWhiteSpace(Search))
            predicate = predicate
                .And(x => x.Name.Contains(Search))
                .Or(x => x.DocumentNumber.Contains(Search))
                .Or(x => x.Email.Contains(Search));
        return predicate;
    }
}