using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCLanche.Models;

// Criamos um apelido fixo para enganar o formatador do Visual Studio
using MeuUserManager = Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser>;
using MeuSignInManager = Microsoft.AspNetCore.Identity.SignInManager<Microsoft.AspNetCore.Identity.IdentityUser>;

namespace MVCLanche.Controllers;

public class AccountController : Controller
{
    private readonly MeuUserManager _userManager;
    private readonly MeuSignInManager _signInManager;

    public AccountController(MeuUserManager userManager, MeuSignInManager signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.UserName,
            model.Password,
            false,
            false);

        if (result.Succeeded)
        {
            if (await _userManager.IsInRoleAsync(
                await _userManager.FindByNameAsync(model.UserName),
                "Admin"))
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Falha na tentativa de login. Verifique os dados.");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new IdentityUser
        {
            UserName = model.UserName,
            Email = model.UserName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Adiciona automaticamente o usuário à Role Member
            await _userManager.AddToRoleAsync(user, "Member");

            // Faz login automaticamente
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();

        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}