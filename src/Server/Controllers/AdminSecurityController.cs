using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Controllers;

/// <summary>
/// Controlador administrativo para operaciones de seguridad sobre sesiones y cookies.
/// </summary>
[ApiController]
[Route("api/admin/security-stamp")]
[Authorize(Roles = "Admin")]
public class AdminSecurityController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IDbContextFactory<AppDbContext> _dbFactory;
    private readonly ILogger<AdminSecurityController> _logger;

    public AdminSecurityController(
        UserManager<ApplicationUser> userManager,
        IDbContextFactory<AppDbContext> dbFactory,
        ILogger<AdminSecurityController> logger)
    {
        _userManager = userManager;
        _dbFactory = dbFactory;
        _logger = logger;
    }

    /// <summary>
    /// Fuerza la regeneración del Security Stamp del usuario Admin autenticado.
    /// Esto invalida la cookie actual y obliga a que se emita una nueva con 2FA actualizado.
    /// </summary>
    [HttpPost("refresh-self")]
    public async Task<IActionResult> RefreshSecurityStampForCurrentUser()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized(new { error = "Usuario no autenticado" });
        }

        var result = await _userManager.UpdateSecurityStampAsync(user);
        if (!result.Succeeded)
        {
            _logger.LogWarning("No se pudo actualizar Security Stamp para {UserId}", user.Id);
            return StatusCode(500, new { error = "No se pudo actualizar Security Stamp" });
        }

        _logger.LogInformation("Security Stamp actualizado para {UserId}", user.Id);
        return Ok(new { message = "Security Stamp actualizado. Debes cerrar sesión e iniciar nuevamente." });
    }

    /// <summary>
    /// Obtiene el estado 2FA del usuario Admin autenticado desde base de datos.
    /// Útil para diagnosticar por qué el banner sigue visible.
    /// </summary>
    [HttpGet("status-self")]
    public async Task<IActionResult> GetTwoFactorStatusForCurrentUser()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new { error = "Usuario no autenticado" });
        }

        await using var db = await _dbFactory.CreateDbContextAsync();
        var appUser = await db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (appUser == null)
        {
            return NotFound(new { error = "Usuario no encontrado en base de datos" });
        }

        return Ok(new
        {
            appUser.Id,
            appUser.Email,
            appUser.TwoFactorEnabled,
            appUser.TwoFactorRequiredSince
        });
    }

    /// <summary>
    /// Habilita 2FA para el usuario Admin autenticado.
    /// Útil cuando el autenticador está verificado pero el flag no quedó en true.
    /// </summary>
    [HttpPost("enable-2fa-self")]
    public async Task<IActionResult> EnableTwoFactorForCurrentUser()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new { error = "Usuario no autenticado" });
        }

        await using var db = await _dbFactory.CreateDbContextAsync();
        var appUser = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (appUser == null)
        {
            return NotFound(new { error = "Usuario no encontrado en base de datos" });
        }

        appUser.TwoFactorEnabled = true;
        db.Users.Update(appUser);
        await db.SaveChangesAsync();

        var identityUser = await _userManager.GetUserAsync(User);
        if (identityUser != null)
        {
            await _userManager.UpdateSecurityStampAsync(identityUser);
        }

        _logger.LogInformation("2FA habilitado para {UserId}", appUser.Id);
        return Ok(new
        {
            message = "2FA habilitado. Debes cerrar sesión e iniciar nuevamente.",
            appUser.Id,
            appUser.Email,
            appUser.TwoFactorEnabled,
            appUser.TwoFactorRequiredSince
        });
    }
}
