using mmc.AccesoDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmc.Modelos;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos.IglesiaModels;

namespace mmc.AccesoDatos.Repositorios
{
    public class CasaEstudioBiblicoRepositorio : Repositorio<CasaEstudioBiblico>, ICasaEstudioBiblicoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CasaEstudioBiblicoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(CasaEstudioBiblico oCasaEstudioBiblico)
        {
            var CasaEstudioBiblicoDB = _db.CasasEstudioBiblico.FirstOrDefault(p => p.Id == oCasaEstudioBiblico.Id);
            if (CasaEstudioBiblicoDB != null)
            {
                if (oCasaEstudioBiblico.ImagenUrl != null)
                {
                    CasaEstudioBiblicoDB.ImagenUrl = oCasaEstudioBiblico.ImagenUrl;
                }

                CasaEstudioBiblicoDB.Id = oCasaEstudioBiblico.Id;
                CasaEstudioBiblicoDB.Fecha = oCasaEstudioBiblico.Fecha;
                CasaEstudioBiblicoDB.TotalCristianos = oCasaEstudioBiblico.TotalCristianos;
                CasaEstudioBiblicoDB.NoCristianos = oCasaEstudioBiblico.NoCristianos;
                CasaEstudioBiblicoDB.Ninos = oCasaEstudioBiblico.Ninos;
                CasaEstudioBiblicoDB.total = oCasaEstudioBiblico.total;
                CasaEstudioBiblicoDB.Convertidos = oCasaEstudioBiblico.Convertidos;
                CasaEstudioBiblicoDB.Reconciliados = oCasaEstudioBiblico.Reconciliados;
                CasaEstudioBiblicoDB.Estado = oCasaEstudioBiblico.Estado;
                CasaEstudioBiblicoDB.TipoCebId = oCasaEstudioBiblico.TipoCebId;
                CasaEstudioBiblicoDB.MiembrosCEBid = oCasaEstudioBiblico.MiembrosCEBid;
            }
        }
    }
}
