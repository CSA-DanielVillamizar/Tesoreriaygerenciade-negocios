using MudBlazor;

namespace Server.Services;

public class LamaToastService
{
    private readonly ISnackbar _snackbar;

    public LamaToastService(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }

    // Métodos principales (con soporte opcional de título)
    public void ShowSuccess(string message, string? title = null)
    {
        var msg = string.IsNullOrEmpty(title) ? message : $"{title}: {message}";
        _snackbar.Add(msg, Severity.Success);
    }

    public void ShowError(string message, string? title = null)
    {
        var msg = string.IsNullOrEmpty(title) ? message : $"{title}: {message}";
        _snackbar.Add(msg, Severity.Error);
    }

    public void ShowInfo(string message, string? title = null)
    {
        var msg = string.IsNullOrEmpty(title) ? message : $"{title}: {message}";
        _snackbar.Add(msg, Severity.Info);
    }

    public void ShowWarning(string message, string? title = null)
    {
        var msg = string.IsNullOrEmpty(title) ? message : $"{title}: {message}";
        _snackbar.Add(msg, Severity.Warning);
    }

    // Sobrecargas comunes para compatibilidad (1 o 2 argumentos)
    public void Success(string message, string? title = null) => ShowSuccess(message, title);
    public void Error(string message, string? title = null) => ShowError(message, title);
    public void Info(string message, string? title = null) => ShowInfo(message, title);
    public void Warning(string message, string? title = null) => ShowWarning(message, title);
}

