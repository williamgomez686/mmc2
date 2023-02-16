using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.Setegua
{
    public class Editorial:BaseModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Nombre de la Editorial")]
        public string Nombre { get; set; }
    }
}
