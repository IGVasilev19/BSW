using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Service;

public class AccessController : Controller
{
    [Route("access/sign-up")]
    public IActionResult SignUp()
    {
        return View();
    }

    [Route("access/sign-in")]
    public IActionResult SignIn()
    {
        return View();
    }
}
