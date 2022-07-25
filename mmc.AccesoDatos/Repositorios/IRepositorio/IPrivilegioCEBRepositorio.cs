﻿using mmc.Modelos;
using mmc.Modelos.IglesiaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios.IRepositorio
{
    public interface IPrivilegioRepositorio : IRepositorio<PrivilegioCEB>
    {
        void Actualizar(PrivilegioCEB privilegioCEB);
    }
}
