using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.DAO.Repositories;
using ToDoList.Models;
using ToDoList.Models.ViewModels;

namespace ToDoList.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Users = _userRepository.GetAll();
            return View(Users);
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

            // Se le asiga el rol de user por defecto
            user.RoleId = 2;

            // Si el usuario no existe, procedemos a hashear la contraseña y crear el usuario
            string PasswordHashed = _passwordHasher.HashPassword(user, user.UserPassword);
            user.UserPassword = PasswordHashed;

            _userRepository.create(user);

            if (user.UserId != 0)
            {
                return RedirectToAction("Login", "User");
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
            return RedirectToAction("Bienvenida", "Paginas");
        }
    }
}
