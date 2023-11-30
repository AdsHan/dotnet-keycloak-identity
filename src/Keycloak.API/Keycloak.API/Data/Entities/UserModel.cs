using Keycloak.API.Data.DomainObjects;

namespace Keycloak.API.Data.Entities;

public class UserModel : BaseEntity
{

    // EF Construtor
    public UserModel()
    {

    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string IdentityId { get; set; }
}
