using mmc.Modelos.IglesiaModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "Usuario")]
        public string UsuarioAplicacionId { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Asunto")]
        public string Asunto { get; set; }
        [Required]
        [MaxLength(2000)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }

        [MaxLength(400)]
        [Display(Name = "Solucion")]
        public string Solucion { get; set; }
        public DateTime? FechaSolucion { get; set; }

        [MaxLength(50)]
        [Display(Name = "Tecnico")]
        public string Tecnico { get; set; }

        public string ImagenURL { get; set; }     
    }
}

public class EstadosTK: CebBase
{
    public int Id { get; set; }
    public string Descripcion { set; get; }

}
public class Archivo
{
    [Key]
    public int id { get; set; }
    public string? nombre { get; set; }
    public string? extension { get; set; }
    public double? tamanio { get; set; }
    public string? ubicacion { get; set; }
}