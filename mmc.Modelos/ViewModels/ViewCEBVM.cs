using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels
{
    public class ViewCEBVM
    {
            public int id { get; set; }
            public DateTime fecha { get; set; }
            public string hora { get; set; }
            public int totalCristianos { get; set; }
            public int noCristianos { get; set; }
            public int ninos { get; set; }
            public int total { get; set; }
            public int convertidos { get; set; }
            public int reconciliados { get; set; }
            public int ofrenda { get; set; }
            public bool estado { get; set; }
            public object imagenUrl { get; set; }
            public string tipo { get; set; }
            public string name { get; set; }
    }
}
