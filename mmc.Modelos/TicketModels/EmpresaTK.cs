using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mmc.Modelos.TicketModels
{
    public class EmpresaTK:BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        public static implicit operator EmpresaTK(int v)
        {
            throw new NotImplementedException();
        }
    }
}
