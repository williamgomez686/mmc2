using System.ComponentModel.DataAnnotations;

namespace mmc.Modelos.ViewModels
{
    public class RegionFromVM
    {
        public int id { get; set; }
        [Display(Name = "Region")]
        public string NombreRegion { get; set; }

        [Display(Name = "Nombre")]
        public string NombreServidor { get; set; }

        [Display(Name = "Apellido")]
        public string ApellidoServidor { get; set; }

        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        [Display(Name = "Cargo")]
        public string Cargos { get; set; }
    }
}
