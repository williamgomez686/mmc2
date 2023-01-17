using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mmc.AccesoDatos.Data;
using mmc.Modelos;
using mmc.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Admin.Controllers
{
    [Area("Admin")]//indicamos a que area pertenese
    [Authorize(Roles = DS.Role_Admin)]
    public class UsuarioController : Controller
    {
        //Declaramos una variable que va a ser nuestro contexto heredando del dbcontex
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        //Inicializamos el constructor para la conexion a la DB
        public UsuarioController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Roles()
        {
             var test =_db.Roles.ToList();

            return View();
        }
        [HttpGet]
        public IActionResult UpSert(string? id)
        {
            var rol = new Rol();
            if (id == null)
            {
                // Esto es para Crear nuevo Registro
                return View(rol);
            }
            // Esto es para Actualizar
            var model = _db.Roles.Where(r => r.Id == id).FirstOrDefault();
            if (model == null)
            {
                return NotFound();
            }
            rol.Name = model.Name;
            rol.NormalizedName = model.NormalizedName;
            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Rol Model)
        {
            if (ModelState.IsValid)
            {
                if (Model.Id == null)
                {
                    //Microsoft.EntityFrameworkCore.ChangeTracking.
                    //    EntityEntry<Microsoft.AspNetCore.Identity.IdentityRole> entityEntry = _db.Roles.Add(Model);

                    if (!await _roleManager.RoleExistsAsync(Model.Name)) //consulta en nuestro proyecto si el rol exite
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Model.Name));
                    }
                }
                else
                {
                    await _roleManager.UpdateAsync(new IdentityRole(Model.Name));
                }
                return RedirectToAction(nameof(Roles));
            }
            return View(Model);
        }
        #region API

        [HttpGet]
        public IActionResult ObtenerRol()
        {
            var userRole = _db.UserRoles.ToList();//esta variable tendra todos los roles por usuario
            var roles = _db.Roles.ToList();//en esta variable optendremos todos los roles que esten el la Db

            return Json(new { data = roles });
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var usuarioLista = _db.UsuarioAplicacion.ToList();//obtenemos todos los usuarios de la tabla usuarios
            var userRole = _db.UserRoles.ToList();//esta variable tendra todos los roles por usuario
            var roles = _db.Roles.ToList();//en esta variable optendremos todos los roles que esten el la Db

            foreach (var usuario in usuarioLista)
            {
                var rolId = userRole.FirstOrDefault(u => u.UserId == usuario.Id).RoleId;//que rol tiene determinado usuario y solo devolvemos el RoleId
                usuario.role = roles.FirstOrDefault(u => u.Id == rolId).Name;//
            }
            return Json(new { data = usuarioLista });//retornamos un Json ya que sera resivida por JavaScript
        }

        [HttpPost]
        public IActionResult BloquearDesbloquear([FromBody] string id)
        {
            var usuario = _db.UsuarioAplicacion.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Error de Usuario" });
            }

            if (usuario.LockoutEnd != null && usuario.LockoutEnd > DateTime.Now)
            {
                // Usuario Bloqueado
                usuario.LockoutEnd = DateTime.Now;//desbloqueando usuario
            }
            else
            {
                usuario.LockoutEnd = DateTime.Now.AddYears(1000);///se agregan 1000 años para bloquear usuarios esto por que asi esta pensado identity
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Operacion Exitosa" });

        }


        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            //var MarcaDB = _unidadTrabajo.Marca.Obtener(id);
            var model = _db.Roles.Where(r => r.Id == id).FirstOrDefault();
            if (model == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            //_unidadTrabajo.Marca.Remover(model);
            await _roleManager.DeleteAsync(new IdentityRole(model.Id));
            return Json(new { success = true, message = "Marca Borrada Exitosamente" });
        }
        #endregion
    }
}
