using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Oracle.DataAccess.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Visitante> Visitantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseOracle("User Id=C##vinculosestrategicos;Password=Vinculo@2025;Data Source=localhost:1521/xe;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##VINCULOSESTRATEGICOS")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008320");

            entity.ToTable("MENU_ITEMS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.PadreId)
                .HasColumnType("NUMBER")
                .HasColumnName("PADRE_ID");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URL");

            entity.HasOne(d => d.Padre).WithMany(p => p.InversePadre)
                .HasForeignKey(d => d.PadreId)
                .HasConstraintName("SYS_C008321");
        });

        modelBuilder.Entity<Visitante>(entity =>
        {
            entity.HasKey(e => e.Dui).HasName("SYS_C008322");

            entity.ToTable("VISITANTES");

            entity.Property(e => e.Dui)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DUI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_NACIMIENTO");
            entity.Property(e => e.Generacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GENERACION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
        });
        modelBuilder.HasSequence("MENU_ITEMS_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
