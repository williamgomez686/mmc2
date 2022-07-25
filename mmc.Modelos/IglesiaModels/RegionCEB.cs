using System;
using System.ComponentModel.DataAnnotations;

namespace mmc.Modelos.IglesiaModels
{
    public class RegionesCEB
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Prompt = "Nombre de la region", Name = "Region")]
        public string RegionName { get; set; }
        [Display(Prompt = "Direccioni de la region", Name = "Direccion")]
        public string RegionAddress { get; set; }
        [Display(Name = "Fecha")]
        public DateTime? Date { get; set; }
        [Display(Name = "Usuario Alta")]
        public string? hihgUser { get; set; }
        [Display(Name = "Activo")]
        public bool Estado { get; set; }
        //public List<user> EncargadoCEB { get; set; }

        //public List<CasaEstudioBiblico> CEB { get; set; }
    }
}
