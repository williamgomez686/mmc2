using Microsoft.AspNetCore.Mvc.Rendering;
using mmc.Modelos.IglesiaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels.IglesiaVM
{
    public class Ceb_CabVM
    {
        public CEB_CAB CasaEstudioCab { get; set; }
        public IEnumerable<SelectListItem> TipoCEBLista { get; set; }
        public IEnumerable<SelectListItem> MiembroCEBLista { get; set; }
    }
}
