using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using mmc.Utilidades;

namespace mmc.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        //agregamos una variable para poder administrar los roles en esta parte
        private readonly RoleManager<IdentityRole> _roleManager;
        //Agregamos nuestra unidad de trabajo 
        private readonly IUnidadTrabajo _unidadTrabajo;

        public RegisterModel(// este es el constructor 
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            //Agregamos nuestras variables en el constructor
            RoleManager<IdentityRole> roleManager,
            IUnidadTrabajo unidadTrabajo
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            //Inisialilzamos las variables que declaramos en el constructor
            _roleManager = roleManager;
            _unidadTrabajo = unidadTrabajo;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(15, MinimumLength = 5)]
            [Display(Name = "Usuario")]
            public string UserName { get; set; }

            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string nombre { get; set; }
            [Required]
            public string apellido { get; set; }
            public string empresa { get; set; }
            public string departamento { get; set; }
            public int extencion { get; set; }
            public string role { get; set; }
            //Agreagamos una variable para la lista del combobox de los roles 
            public IEnumerable<SelectListItem> ListaRol { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            //inicialisamos la variable lista para los roles 
            Input = new InputModel()
            {// esta lista la saca del rolmanager con la condicion que sea distito al rol del cliente 
                ListaRol = _roleManager.Roles.Where(r => r.Name != DS.Role_Cliente).Select(n => n.Name).Select(l => new SelectListItem
                {
                    Text = l,// va a contener el nombre de los roles
                    Value = l //va a contener el valor de los roles
                })
            };

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)//Verifica si el Modelo es Valido
            {
                //var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };//hace referencia al IdentityUser nosotros lo oviamos
                var user = new UsuarioAplicacion // creamos una variable con el modelo que creamos 
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    nombre = Input.nombre,
                    apellido = Input.apellido,
                    empresa = Input.empresa,
                    departamento = Input.departamento,
                    PhoneNumber = Input.PhoneNumber,
                    extencion = Input.extencion,
                    role = Input.role
                };

                
                var result = await _userManager.CreateAsync(user, Input.Password);//aqui se hace el ingreso llamando a nuestra variable "User" creada arriba 
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //SE crean los roles
                    //primero pregunta si el rol existe si no existe lo va a crear
                    if (!await _roleManager.RoleExistsAsync(DS.Role_Admin)) //consulta en nuestro proyecto si el rol exite
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_Admin));
                    }
                    if (!await _roleManager.RoleExistsAsync(DS.Role_Cliente)) //consulta en nuestro proyecto si el rol exite
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_Cliente));
                    }
                    if (!await _roleManager.RoleExistsAsync(DS.Role_Ticket)) //consulta en nuestro proyecto si el rol exite
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_Ticket));
                    }
                    if (!await _roleManager.RoleExistsAsync(DS.Role_SeteguaBiblioteca)) //consulta en nuestro proyecto si el rol exite
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_SeteguaBiblioteca));
                    }

                    //Se asigna el Rol administrador esto provisional **************************************************
                    //await _userManager.AddToRoleAsync(user, DS.Role_Admin);      

                    if (user.role == null)
                    {
                        await _userManager.AddToRoleAsync(user, DS.Role_Cliente);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, user.role);
                    }


                    //ESTE FRAGMENTO DE CONDIGO SIRVE PARA ENVIAR UN EMAIL DE CONFIRMACION DESPUES DE CREAR UN USUARIO
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email", 
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if(user.role == null)
                        {   //se esta creando un nuevo usuario
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            //Administrador esta creando un nuevo usuario y lo redirecciona a la vista de administracion de usuarios
                            return RedirectToAction("Index", "Usuario", new { Area = "Admin" });
                        }
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
