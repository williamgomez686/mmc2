﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using mmc.AccesoDatos.Data;

namespace mmc.AccesoDatos.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("mmc.Modelos.Bodega", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("Estado")
                        .HasColumnType("boolean");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Bodegas");
                });

            modelBuilder.Entity("mmc.Modelos.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Estado")
                        .HasColumnType("boolean");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("mmc.Modelos.IglesiaModels.CasaEstudioBiblico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Convertidos")
                        .HasColumnType("integer");

                    b.Property<bool>("Estado")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Hora")
                        .HasColumnType("text");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("text");

                    b.Property<int>("MiembrosCEBid")
                        .HasColumnType("integer");

                    b.Property<int>("Ninos")
                        .HasColumnType("integer");

                    b.Property<int>("NoCristianos")
                        .HasColumnType("integer");

                    b.Property<double>("Ofrenda")
                        .HasColumnType("double precision");

                    b.Property<int>("Reconciliados")
                        .HasColumnType("integer");

                    b.Property<int>("TipoCebId")
                        .HasColumnType("integer");

                    b.Property<int>("TotalCristianos")
                        .HasColumnType("integer");

                    b.Property<int>("total")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MiembrosCEBid");

                    b.HasIndex("TipoCebId");

                    b.ToTable("CasasEstudioBiblico");
                });

            modelBuilder.Entity("mmc.Modelos.IglesiaModels.MiembrosCEB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Addres")
                        .HasColumnType("text");

                    b.Property<int>("CargosCEBId")
                        .HasColumnType("integer");

                    b.Property<string>("DPI")
                        .HasColumnType("text");

                    b.Property<bool>("Estado")
                        .HasColumnType("boolean");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RegionId")
                        .HasColumnType("integer");

                    b.Property<string>("lastName")
                        .HasColumnType("text");

                    b.Property<string>("phone")
                        .HasColumnType("text");

                    b.Property<string>("phone2")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CargosCEBId");

                    b.HasIndex("RegionId");

                    b.ToTable("Miembros");
                });

            modelBuilder.Entity("mmc.Modelos.IglesiaModels.PrivilegioCEB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Cargos")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Estado")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("privilegios");
                });

            modelBuilder.Entity("mmc.Modelos.IglesiaModels.RegionesCEB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Estado")
                        .HasColumnType("boolean");

                    b.Property<string>("RegionAddress")
                        .HasColumnType("text");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("hihgUser")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RegionesCEB");
                });

            modelBuilder.Entity("mmc.Modelos.IglesiaModels.TiposCEB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Estado")
                        .HasColumnType("boolean");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("TiposCEB");
                });

            modelBuilder.Entity("mmc.Modelos.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Estado")
                        .HasColumnType("boolean");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Marca");
                });

            modelBuilder.Entity("mmc.Modelos.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("integer");

                    b.Property<double>("Costo")
                        .HasColumnType("double precision");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("text");

                    b.Property<int>("MarcaId")
                        .HasColumnType("integer");

                    b.Property<string>("NumeroSerie")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int?>("PadreId")
                        .HasColumnType("integer");

                    b.Property<double>("Precio")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("MarcaId");

                    b.HasIndex("PadreId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("mmc.Modelos.estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("est_descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("est_est")
                        .HasMaxLength(20)
                        .HasColumnType("boolean");

                    b.Property<DateTime>("est_fchalt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("est_usu_alt")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("estado");
                });

            modelBuilder.Entity("mmc.Modelos.UsuarioAplicacion", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("departamento")
                        .HasColumnType("text");

                    b.Property<string>("empresa")
                        .HasColumnType("text");

                    b.Property<int>("extencion")
                        .HasColumnType("integer");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("UsuarioAplicacion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("mmc.Modelos.IglesiaModels.CasaEstudioBiblico", b =>
                {
                    b.HasOne("mmc.Modelos.IglesiaModels.MiembrosCEB", "MiembroLider")
                        .WithMany()
                        .HasForeignKey("MiembrosCEBid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mmc.Modelos.IglesiaModels.TiposCEB", "TipoCEB")
                        .WithMany()
                        .HasForeignKey("TipoCebId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MiembroLider");

                    b.Navigation("TipoCEB");
                });

            modelBuilder.Entity("mmc.Modelos.IglesiaModels.MiembrosCEB", b =>
                {
                    b.HasOne("mmc.Modelos.IglesiaModels.PrivilegioCEB", "cargo")
                        .WithMany()
                        .HasForeignKey("CargosCEBId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mmc.Modelos.IglesiaModels.RegionesCEB", "region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cargo");

                    b.Navigation("region");
                });

            modelBuilder.Entity("mmc.Modelos.Producto", b =>
                {
                    b.HasOne("mmc.Modelos.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mmc.Modelos.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mmc.Modelos.Producto", "Padre")
                        .WithMany()
                        .HasForeignKey("PadreId");

                    b.Navigation("Categoria");

                    b.Navigation("Marca");

                    b.Navigation("Padre");
                });
#pragma warning restore 612, 618
        }
    }
}
