using System.ComponentModel.DataAnnotations;

namespace mmc.Modelos.IglesiaModels
{
    public class PrivilegioCEB
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        //[Display(Name = "Cargo")]
        [Display(Prompt = "Ingrese en Nombre de privilegio", Name = "Cargo")]
        [MinLength(5, ErrorMessage = "La longitud mínima es 10")]
        public string Cargos { get; set; }
        [Display(Name ="Activo")]    
        public bool Estado { get; set; }
    }
}
