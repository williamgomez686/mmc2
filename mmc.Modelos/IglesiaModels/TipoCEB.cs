using System.ComponentModel.DataAnnotations;

namespace mmc.Modelos.IglesiaModels
{
    public class TiposCEB
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Tipo CEB")]
        public string Tipo { get; set; }
        [Display(Name ="Activo")]
        public bool Estado { get; set; }
    }
}
