using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.Setegua
{
    public class Autores:BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellidos")]
        public string Apellido { get; set; }
        [Display(Name = "Pais")]
        public string Pais { get; set; }
    }
}
