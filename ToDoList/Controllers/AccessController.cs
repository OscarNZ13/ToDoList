using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.DAO.Repositories;
using ToDoList.Models.ViewModels;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public AccessController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            // Verificar si el nombre de usuario ya existe en la base de datos
            var existingUser = _userRepository.GetUserByName(user.UserName).Result;
            if (existingUser != null)
            {
                // Si el usuario ya existe, mostramos un mensaje de error
                ViewData["Mensaje"] = "Elija otro nombre de usuario";
                return View();
            }

            // Se le asiga un rol y estado por defecto
            user.RoleId = 2;
            user.StateId = 1;

            // Si el usuario no existe, procedemos a hashear la contraseña y crear el usuario
            string PasswordHashed = _passwordHasher.HashPassword(user, user.UserPassword);
            user.UserPassword = PasswordHashed;

            _userRepository.create(user);

            if (user.UserId != 0)
            {
                return RedirectToAction("Login", "Access");
            }
            else
            {
                ViewData["Mensaje"] = "No se pudo crear el usuario";
                return View();
            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserVM uservm)
        {
            // Verificar si el usuario ya existe en la base de datos
            var existingUser = _userRepository.GetUserByName(uservm.UserName).Result;

            // Si el usuario no existe entraria en esta validacion
            if (existingUser == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

            //Si es encontrado se verificarian la contrasena
            //Parametros(variable de la db, contrasena del usuario de la db, contrasena del usuario que se ingreso)
            var verificationPasswords = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.UserPassword, uservm.UserPassword);

            //verificamos el resultado de la comparacion de las contrasenas
            if (verificationPasswords != PasswordVerificationResult.Success)
            {
                ViewData["Mensaje"] = "Contraseña incorrecta";
                return View();
            }

            // Creacion de la secion y guardado de informacion de usuario logueado 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.UserId.ToString()),
                new Claim(ClaimTypes.Name, existingUser.UserName),
                new Claim(ClaimTypes.Role, existingUser.RoleId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties { AllowRefresh = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
