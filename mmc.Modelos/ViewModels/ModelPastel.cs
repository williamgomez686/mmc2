using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels
{
    public class ModelPastel
    {
        //name: 'Chrome',
        //    y: 61.41,
        //    sliced: true,
        //    selected: true

        public string name { get; set; }
        public double y { get; set; }
        public bool sliced { get; set; }
        public bool selected { get; set; }

        public ModelPastel()
        {

        }

        public ModelPastel(string name, double y, bool sliced = false, bool selected = false)
        {
            this.name = name;
            this.y = y;
            this.sliced = sliced;
            this.selected = selected;
        }

        public List<ModelPastel> GetDataDummy()
        {
            List<ModelPastel> lista = new List<ModelPastel>();

            lista.Add(new ModelPastel("Angular", 45));
            lista.Add(new ModelPastel("VueJS", 50));
            lista.Add(new ModelPastel("ReactJS", 60));
            lista.Add(new ModelPastel("CSS3", 34));
            lista.Add(new ModelPastel("HTML5", 20));

            return lista;
        }
    }
}
