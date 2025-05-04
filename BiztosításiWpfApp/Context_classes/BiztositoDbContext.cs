using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BiztositasKezelo.Context_classes;

public partial class BiztositoDbContext : DbContext
{
    public BiztositoDbContext()
    {
    }

    public BiztositoDbContext(DbContextOptions<BiztositoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Biztosito> Biztosito { get; set; }

    public DbSet<Felhasznalo> Felhasznalo { get; set; }

    public DbSet<Karesemeny> Karesemeny { get; set; }

    public DbSet<Szemely> Szemely { get; set; }

    public DbSet<Szerzodes> Szerzodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-MGUV9SMU\\SQLEXPRESS;Initial Catalog=Biztosito_db;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Biztosito>(entity =>
        {
            entity.ToTable("Biztosito");

            entity.Property(e => e.BiztositoId).HasColumnName("biztosito_id");
            entity.Property(e => e.Nev).HasColumnName("nev");
            entity.Property(e => e.Tipus).HasColumnName("tipus");
        });

        modelBuilder.Entity<Felhasznalo>(entity =>
        {
            entity.ToTable("Felhasznalo");

            entity.Property(e => e.FelhasznaloId).HasColumnName("felhasznalo_id");
            entity.Property(e => e.FelhNev)
                .HasMaxLength(50)
                .HasColumnName("felh_nev");
            entity.Property(e => e.Jelszo).HasColumnName("jelszo");
            entity.Property(e => e.Jogosultsag).HasColumnName("jogosultsag");
        });

        modelBuilder.Entity<Karesemeny>(entity =>
        {
            entity.ToTable("Karesemeny");

            entity.Property(e => e.KaresemenyId).HasColumnName("karesemeny_id");
            entity.Property(e => e.Megnevezes).HasColumnName("megnevezes");
            entity.Property(e => e.SzerzId).HasColumnName("szerz_id");

            entity.HasOne(d => d.Szerz).WithMany(p => p.Karesemeny)
                .HasForeignKey(d => d.SzerzId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Karesemeny_Szerzodes");
        });

        modelBuilder.Entity<Szemely>(entity =>
        {
            entity.ToTable("Szemely");

            entity.Property(e => e.SzemelyId).HasColumnName("szemely_id");
            entity.Property(e => e.FelhId).HasColumnName("felh_id");
            entity.Property(e => e.SzulDatum).HasColumnName("szul_datum");
            entity.Property(e => e.Utonev)
                .HasMaxLength(50)
                .HasColumnName("utonev");
            entity.Property(e => e.Varos)
                .HasMaxLength(50)
                .HasColumnName("varos");
            entity.Property(e => e.Veznev)
                .HasMaxLength(50)
                .HasColumnName("veznev");

            entity.HasOne(d => d.Felh).WithMany(p => p.Szemely)
                .HasForeignKey(d => d.FelhId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Szemely_Felhasznalo");
        });

        modelBuilder.Entity<Szerzodes>(entity =>
        {
            entity.HasKey(e => e.SzerzodesId);

            entity.Property(e => e.SzerzodesId).HasColumnName("szerzodes_id");
            entity.Property(e => e.BiztId).HasColumnName("bizt_id");
            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.FelhId).HasColumnName("felh_id");
            entity.Property(e => e.Honap).HasColumnName("honap");
            entity.Property(e => e.Osszeg)
                .HasMaxLength(50)
                .HasColumnName("osszeg");

            entity.HasOne(d => d.Bizt).WithMany(p => p.Szerzodes)
                .HasForeignKey(d => d.BiztId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Szerzodes_Biztosito");

            entity.HasOne(d => d.Felh).WithMany(p => p.Szerzodes)
                .HasForeignKey(d => d.FelhId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Szerzodes_Szerzodes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
