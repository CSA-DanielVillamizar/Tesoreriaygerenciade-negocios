using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Models;

namespace Server.Areas.Identity.Pages.Account.Manage;

/// <summary>
/// Página de administración de autenticación de dos factores (2FA).
/// Expone estado de 2FA y acciones básicas de configuración.
/// </summary>
public class TwoFactorAuthenticationModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public TwoFactorAuthenticationModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Indica si el usuario tiene 2FA habilitado.
    /// </summary>
    public bool Is2faEnabled { get; private set; }

    /// <summary>
    /// Cantidad de códigos de recuperación disponibles.
    /// </summary>
    public int RecoveryCodesLeft { get; private set; }

    /// <summary>
    /// Mensaje de estado para UI.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; } = string.Empty;

    /// <summary>
    /// Carga el estado actual de 2FA del usuario autenticado.
    /// </summary>
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"No se pudo cargar el usuario con ID '{_userManager.GetUserId(User)}'.");
        }

        Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
        RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

        return Page();
    }
}
