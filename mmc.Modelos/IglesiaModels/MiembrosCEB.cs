using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mmc.Modelos.IglesiaModels
{
    public class MiembrosCEB
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Nombres")]
        public string Name { get; set; }
        [Display(Name = "Apellidos")]
        public string lastName { get; set; }
        [Display(Name = "Direccion")]
        public string Addres { get; set; }
        [Display(Name = "Telefono")]
        public string phone { get; set; }
        [Display(Name = "Telefono 2")]
        public string? phone2 { get; set; }
        [Display(Name = "DPI")]
        public string DPI { get; set; }
        public bool Estado { get; set; }

        public string? ImagenUrl { get; set; }

        //llavaes
        public int CargosCEBId { get; set; }
        [ForeignKey("CargosCEBId")]
        public PrivilegioCEB cargo { get; set; }

        public int RegionId { get; set; }
        [ForeignKey("RegionId")]
        public RegionesCEB region { get; set; }
    }
}
