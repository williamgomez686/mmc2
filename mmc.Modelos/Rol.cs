using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos
{
    public class Rol
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
