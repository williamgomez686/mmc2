using Microsoft.AspNetCore.Mvc.Rendering;
using mmc.Modelos.IglesiaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels
{
    public class MiembroVM
    {
        public MiembrosCEB Miembros { get; set; }
        public IEnumerable<SelectListItem> RegionCEBLista { get; set; }
        public IEnumerable<SelectListItem> PrivilegioCEBLista { get; set; }

    }
}
