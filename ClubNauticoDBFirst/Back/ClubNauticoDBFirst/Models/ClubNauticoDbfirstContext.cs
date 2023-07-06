using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClubNauticoDBFirst.Models;

public partial class ClubNauticoDbfirstContext : DbContext
{
    public ClubNauticoDbfirstContext()
    {
    }

    public ClubNauticoDbfirstContext(DbContextOptions<ClubNauticoDbfirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Barco> Barcos { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=club_nautico_DBFirst;Port=5432;User Id=geraValdez;Password=123456;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Barco>(entity =>
        {
            entity.HasKey(e => e.IdBarco).HasName("pk_barcos");

            entity.ToTable("barcos");

            entity.Property(e => e.IdBarco)
                .ValueGeneratedNever()
                .HasColumnName("idBarco");
            entity.Property(e => e.Cuota).HasColumnName("cuota");
            entity.Property(e => e.IdSocio).HasColumnName("idSocio");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.NroAmarre).HasColumnName("nroAmarre");
            entity.Property(e => e.NroMatricula).HasColumnName("nroMatricula");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.Barcos)
                .HasForeignKey(d => d.IdSocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_barcos_socios");
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.IdSocio).HasName("pk_socios");

            entity.ToTable("socios");

            entity.Property(e => e.IdSocio)
                .UseIdentityAlwaysColumn()
                .HasColumnName("idSocio");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Apellido).HasColumnName("apellido");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Telefono).HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
