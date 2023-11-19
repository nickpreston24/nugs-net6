using CodeMechanic.Extensions;

namespace nugsnet6.Models;

public class AlertModel
{
    public string Message { get; set; } = string.Empty;
    public Maybe<Exception> Error { get; set; } = Maybe<Exception>.None;

    public string Kind => this.Error
        .Case(some: (_) => "alert-fail"
            , none: () => "alert-success");
}