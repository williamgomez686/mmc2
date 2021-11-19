using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos
{
    public class UsuarioAplicacion : IdentityUser
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }

        public string empresa { get; set; }
        public string departamento { get; set; }
        public int extencion { get; set; }
        //Esta variable no se guardara en la DB sino que sera unicamente de referencia en el modelo 
        [NotMapped]
        public string role { get; set; }
    }
}
