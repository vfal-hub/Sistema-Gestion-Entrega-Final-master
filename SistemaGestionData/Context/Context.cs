﻿using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;

namespace SistemaGestionData.Context;

public class CoderhouseContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<ProductosVendidos> ProductosVendidos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }

    public CoderhouseContext()
        : base() { }

    public CoderhouseContext(DbContextOptions<CoderhouseContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
            "Data Source=Valentino;Initial Catalog=CoderhouseCapas;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"
        );
    }
    }


}


