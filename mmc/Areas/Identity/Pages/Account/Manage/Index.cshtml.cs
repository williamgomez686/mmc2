using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mmc.AccesoDatos.Data;
using mmc.Modelos;

namespace mmc.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _contex;
        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contex = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Numero de Telefono")]
            public string PhoneNumber { get; set; }

            public string Nombres { get; set; }
            public string Apellidos { get; set; }

            public string Departamento { get; set; }
            public int Extencion { get; set; }

        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var nombres = "";
            var apellidos = "";
            var extencion = 0;
            var departamento = "";

            Username = userName;
            //importamos los datos de los usuarios por medio de las claims 
            var clasimIdentidad = (ClaimsIdentity)User.Identity;
            var claim = clasimIdentidad.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var UsuarioApp = await _contex.UsuarioAplicacion.FindAsync(claim.Value);
                nombres = UsuarioApp.nombre;
                apellidos = UsuarioApp.apellido;
                departamento = UsuarioApp.departamento;
                extencion = UsuarioApp.extencion;
            }

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nombres= nombres,
                Apellidos = apellidos,
                Departamento = departamento,
                Extencion = extencion       
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var claimIdentidad = (ClaimsIdentity)User.Identity;
            var claim = claimIdentidad.FindFirst(ClaimTypes.NameIdentifier);
            var usuarioApp = new UsuarioAplicacion();

            if(claim != null)
            {
                usuarioApp = await _contex.UsuarioAplicacion.FindAsync(claim.Value);
                usuarioApp.nombre = Input.Nombres;
                usuarioApp.apellido = Input.Apellidos;
                //usuarioApp.departamento = Input.departemento;
            }

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            _contex.UsuarioAplicacion.Update(usuarioApp);
            await _contex.SaveChangesAsync();
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
