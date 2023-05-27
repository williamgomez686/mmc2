using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static mmc.AccesoDatos.Data.ApplicationDbContext;
using mmc.Modelos;
using mmc.Modelos.IglesiaModels;
using mmc.Modelos.TicketModels;
using mmc.Modelos.IglesiaModels.lafamiliadedios;

namespace mmc.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<estado> estado { get; set; }
        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }
        //public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Marca> Marca { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        //***************************iglesia***************************************
        public DbSet<Cl_Peticiones> Peticiones { get; set; }
        public DbSet<TiposCEB> TiposCEB { get; set; }
        public DbSet<PrivilegioCEB> privilegios { get; set; }
        public DbSet<RegionesCEB> RegionesCEB { get; set; }
        public DbSet<MiembrosCEB> Miembros { get; set; }
        public DbSet<CasaEstudioBiblico> CasasEstudioBiblico { get; set; }
        public DbSet<CEB_CAB> CEB_CABs { get; set; }
        public DbSet<CEB_DET> CEB_DETs { get; set; }
        //**************************Igelsia Reuniones *******************************
        public DbSet<IglesiaDepartamentos> IglesiaDepartamentos { get; set; }
        public DbSet<IglesiaReuniones> IglesiaReuniones { get; set; }
        public DbSet<IglesiaServidores> IglesiaServidores { get; set; }
        public DbSet<IglesiaServidoresReunion> IglesiaServidoresReuniones { get; set; } 

        //*******************TICKET*************************************************
        public DbSet<AreaSoporteTK> AreaSoporteTK {get; set;}
        public DbSet<EstadosTK> EstadosTKs { get; set; }
        public DbSet<EmpresaTK> EmpresasTK { get; set; }

        public DbSet<UrgenciaTK> UrgenciasTK { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Usuario>(entityTypeBuilder =>//espesificamos que entidad vamos a modificar
        //    {
        //        entityTypeBuilder.ToTable("Usuarios");//de esta manera renombramos la tabla al nombre que indicamos aqui

        //        entityTypeBuilder.Property(u => u.UserName)
        //            .HasMaxLength(100)
        //            .HasDefaultValue(0);
        //        entityTypeBuilder.Property(u => u.nombre)
        //            .HasMaxLength(60);
        //        entityTypeBuilder.Property(u => u.apaellido)
        //            .HasMaxLength(60);
        //        entityTypeBuilder.Property(u => u.NIT)
        //            .HasMaxLength(60);
        //        entityTypeBuilder.Property(u => u.DPI)
        //            .HasMaxLength(25);
        //        entityTypeBuilder.Property(u => u.sexo)
        //            .HasMaxLength(20);
        //    });
        //}


        //public class Usuario : IdentityUser
        //{
        //    public string nombre { get; set; }
        //    public string apaellido { get; set; }
        //    public string NIT { get; set; }
        //    public string DPI { get; set; }
        //    public string sexo { get; set; }
        //    public DateTime fch_nacimieto { get; set; }
        //}
    }
}

