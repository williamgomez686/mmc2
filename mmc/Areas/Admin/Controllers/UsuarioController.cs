using Microsoft.AspNetCore.Mvc;
using mmc.AccesoDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Admin.Controllers
{
    [Area("Admin")]//indicamos a que area pertenese
    public class UsuarioController : Controller
    {
        //Declaramos una variable que va a ser nuestro contexto heredando del dbcontex
        private readonly ApplicationDbContext _db;
        //Inicializamos el constructor para la conexion a la DB
        public UsuarioController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API

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

        #endregion
    }
}
