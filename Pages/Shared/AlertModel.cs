using CodeMechanic.Extensions;

namespace nugsnet6.Models;

public class AlertModel
{
    public AlertModel(Exception ex = null)
    {
        Error = ex.ToMaybe();
        Message = ex?.Message ?? string.Empty;
    }

    public string Message { get; set; } = string.Empty;
    public Maybe<Exception> Error { get; set; } = Maybe<Exception>.None;

    public string Kind => this.Error
        .Case(some: (_) => "alert-error"
            , none: () => "alert-success");
}