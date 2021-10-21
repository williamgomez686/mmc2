﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos
{
    public class estado
    {
        [Key]
        public int estadoId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Descripcion")]
        public string est_descripcion { get; set; }
        [MaxLength(20)]
        public string est_est { get; set; }
        public DateTime est_fchalt { get; set; }
        [MaxLength(20)]
        public string est_usu_alt { get; set; }
    }
}
