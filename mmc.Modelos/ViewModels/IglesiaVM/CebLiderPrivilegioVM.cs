using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels.IglesiaVM
{
    public class CebLiderPrivilegioVM
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Hora { get; set; }
        public string Dia { get; set; }
        public string Tipo { get; set; }
        public string Cargo { get; set; }
        public int cebid { get; set; }
    }
}
