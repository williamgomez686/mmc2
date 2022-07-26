using Microsoft.AspNetCore.Mvc.Rendering;
using mmc.Modelos.IglesiaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels
{
    public class CEB_VM
    {
        public CasaEstudioBiblico CasaEstudioBiblico { get; set; }
        public IEnumerable<SelectListItem> TipoCEBLista { get; set; }
        public IEnumerable<SelectListItem> MiembroCEBLista { get; set; }

    }
}
