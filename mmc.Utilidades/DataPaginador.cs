using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Utilidades
{
    public class DataPaginador<T>
    {
        public List<T> Lista { get; set; }
        public string Pagi_info { get; set; }
        public string Pagi_navegacion { get; set; }
        public T Modelo { get; set; }
        public int Registros { get; set; }
        public string Search { get; set; }
        public string ErrorMessage { get; set; }
    }
}
