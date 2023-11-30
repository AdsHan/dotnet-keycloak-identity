using MediatR;
using System.Text.Json.Serialization;

namespace Keycloak.API.Common;

public abstract record Command : IRequest<BaseResult>
{
    [JsonIgnore]
    public BaseResult BaseResult { get; set; }

    protected Command()
    {
        BaseResult = new BaseResult();
    }
}
