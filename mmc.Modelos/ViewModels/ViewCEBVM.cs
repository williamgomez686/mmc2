using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels
{
    public class ViewCEBVM
    {
        public int id { get; set; }
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }
        [Display(Name = "Hora")]
        public string hora { get; set; }
        [Display(Name = "Cristianos")]
        public int totalCristianos { get; set; }
        [Display(Name = "Inconversos")]
        public int noCristianos { get; set; }
        [Display(Name = "Niños")]
        public int ninos { get; set; }
        [Display(Name = "Total")]
        public int total { get; set; }
        [Display(Name = "Convertidos")]
        public int convertidos { get; set; }
        [Display(Name = "Reconcilia")]
        public int reconciliados { get; set; }
        [Display(Name = "Ofrenda")]
        public Double ofrenda { get; set; }
        [Display(Name = "Estado")]
        public bool estado { get; set; }
        [Display(Name = "Imagen")]
        public object imagenUrl { get; set; }
        [Display(Name = "Casa")]
        public string tipo { get; set; }
        [Display(Name = "Lider")]
        public string name { get; set; }
    }
}
