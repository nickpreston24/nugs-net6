using Hydro;
using nugsnet6.Models;

namespace nugsnet6.Pages.Components;

[AuthorizeFakesCreation]
public class FakePartForm : HydroComponent
{
    // private readonly IPartsService parts_svc;
    //
    // public FakePartForm(IPartsService parts)
    // {
    //     this.parts_svc = parts;
    // }

    public Part EditedPart { get; set; } = new();

    public async Task Create()
    {
        // await parts_svc.Create(EditedPart);
    }
}

public sealed class AuthorizeFakesCreation : Attribute, IHydroAuthorizationFilter
{
    public Task<bool> AuthorizeAsync(HttpContext httpContext, object component)
    {
        var isAuthorized = httpContext.User.Identity?.IsAuthenticated ?? false;
        return Task.FromResult(isAuthorized);
    }
}
