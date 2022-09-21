using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.IglesiaModels
{
    public class CasaEstudioBiblico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]          
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }

        [Display(Name = "Cristianos")]
        public int TotalCristianos { get; set; }
        [Display(Name = "No Cristianos")]
        public int NoCristianos { get; set; }
        [Display(Name = "Niños")]
        public int Ninos { get; set; }
        [Display(Name = "Total")]
        public int total { get; set; }
        [Display(Name = "Convertidos")]
        public int Convertidos { get; set; }
        public int Reconciliados { get; set; }
        public bool Estado { get; set; }
        [Display(Name = "Ofrenda")]
        public double Ofrenda { get; set; }
        public string? ImagenUrl { get; set; }
        //llave foranea de Tipo *********************************
        [Display(Name = "Tipo")]
        public int TipoCebId { get; set; }
        [ForeignKey("TipoCebId")]
        public TiposCEB TipoCEB { get; set; }

        //llave Foranea de Regional ******************************
        [Display(Name = "Lider")]
        public int MiembrosCEBid { get; set; }
        [ForeignKey("MiembrosCEBid")]
        public MiembrosCEB MiembroLider { get; set; }
    }
}
