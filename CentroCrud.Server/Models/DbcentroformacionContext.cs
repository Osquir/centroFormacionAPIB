using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CentroCrud.Server.Models;

public partial class DbcentroformacionContext : DbContext
{
    public DbcentroformacionContext()
    {
    }

    public DbcentroformacionContext(DbContextOptions<DbcentroformacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<AlumnoCurso> AlumnoCursos { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<RecuperacionPassword> RecuperacionPasswords { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PK__Alumnos__43FBBAC735B24167");

            entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");
            entity.Property(e => e.Nif)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nif");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primerApellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primerNombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("segundoApellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("segundoNombre");
        });

        modelBuilder.Entity<AlumnoCurso>(entity =>
        {
            entity.HasKey(e => e.IdAlumnoCurso).HasName("PK__Alumno_C__850F578F7724CBF2");

            entity.ToTable("Alumno_Curso");

            entity.Property(e => e.IdAlumnoCurso).HasColumnName("idAlumnoCurso");
            entity.Property(e => e.AlumnoId).HasColumnName("alumno_id");
            entity.Property(e => e.CursoId).HasColumnName("curso_id");

            entity.HasOne(d => d.Alumno).WithMany(p => p.AlumnoCursos)
                .HasForeignKey(d => d.AlumnoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alumno_Cu__alumn__4F7CD00D");

            entity.HasOne(d => d.Curso).WithMany(p => p.AlumnoCursos)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alumno_Cu__curso__5070F446");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__Cursos__8551ED05C0B48C6B");

            entity.Property(e => e.IdCurso).HasColumnName("idCurso");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Creditos).HasColumnName("creditos");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Temario)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("temario");
        });

        modelBuilder.Entity<RecuperacionPassword>(entity =>
        {
            entity.HasKey(e => e.IdRecPass).HasName("PK__Recupera__F5A38C4C440940BA");

            entity.ToTable("Recuperacion_Password");

            entity.Property(e => e.IdRecPass).HasColumnName("idRecPass");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("token");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RecuperacionPasswords)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recuperac__usuar__534D60F1");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F76490E2FD6");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A6F7D9F2CA");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioRol).HasName("PK__Usuario___50B09207427D076D");

            entity.ToTable("Usuario_Rol");

            entity.Property(e => e.IdUsuarioRol).HasColumnName("idUsuarioRol");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario_R__rol_i__59063A47");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario_R__usuar__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
