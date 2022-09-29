using Microsoft.AspNetCore.Mvc.Rendering;
using mmc.Modelos.IglesiaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels.IglesiaVM
{
    public class Ceb_DetVM
    {
        public CEB_DET CasaEstudioDet { get; set; }
        public IEnumerable<SelectListItem> Cabeceraid { get; set; }
    }
}
