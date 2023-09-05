using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace partyholic_api.Models
{
    public partial class partyholicContext : DbContext
    {
        public partyholicContext()
        {
        }

        public partyholicContext(DbContextOptions<partyholicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Evento> Eventos { get; set; } = null!;
        public virtual DbSet<Grupo> Grupos { get; set; } = null!;
        public virtual DbSet<GruposLogro> GruposLogros { get; set; } = null!;
        public virtual DbSet<Logro> Logros { get; set; } = null!;
        public virtual DbSet<Mensaje> Mensajes { get; set; } = null!;
        public virtual DbSet<Retar> Retars { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuariosEvento> UsuariosEventos { get; set; } = null!;
        public virtual DbSet<UsuariosGrupo> UsuariosGrupos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;database=partyholic;user id=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => new { e.CodEvento, e.CodGrupo })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasIndex(e => e.CodGrupo, "cod_grupo");

                entity.Property(e => e.CodEvento).HasColumnName("cod_evento");

                entity.Property(e => e.CodGrupo).HasColumnName("cod_grupo");

                entity.Property(e => e.FechaEvento).HasColumnName("fecha_evento");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.CodGrupoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.CodGrupo)
                    .HasConstraintName("Eventos_ibfk_1");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.CodGrupo)
                    .HasName("PRIMARY");

                entity.Property(e => e.CodGrupo).HasColumnName("cod_grupo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FotoGrupo)
                    .HasMaxLength(255)
                    .HasColumnName("foto_grupo");

                entity.Property(e => e.Juego)
                    .HasMaxLength(100)
                    .HasColumnName("juego");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Privacidad)
                    .HasMaxLength(100)
                    .HasColumnName("privacidad");
            });

            modelBuilder.Entity<GruposLogro>(entity =>
            {
                entity.HasIndex(e => e.CodGrupo, "cod_grupo");

                entity.HasIndex(e => e.CodLogro, "cod_logro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actual).HasColumnName("actual");

                entity.Property(e => e.Alcanzado).HasColumnName("alcanzado");

                entity.Property(e => e.CodGrupo).HasColumnName("cod_grupo");

                entity.Property(e => e.CodLogro).HasColumnName("cod_logro");

                entity.HasOne(d => d.CodGrupoNavigation)
                    .WithMany(p => p.GruposLogros)
                    .HasForeignKey(d => d.CodGrupo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("GruposLogros_ibfk_1");

                entity.HasOne(d => d.CodLogroNavigation)
                    .WithMany(p => p.GruposLogros)
                    .HasForeignKey(d => d.CodLogro)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("GruposLogros_ibfk_2");
            });

            modelBuilder.Entity<Logro>(entity =>
            {
                entity.HasKey(e => e.CodLogro)
                    .HasName("PRIMARY");

                entity.Property(e => e.CodLogro).HasColumnName("cod_logro");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Objetivo).HasColumnName("objetivo");
            });

            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.HasKey(e => e.IdMensaje)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CodGrupo, "cod_grupo");

                entity.HasIndex(e => e.Username, "username");

                entity.Property(e => e.IdMensaje).HasColumnName("id_mensaje");

                entity.Property(e => e.CodGrupo).HasColumnName("cod_grupo");

                entity.Property(e => e.Contenido)
                    .HasMaxLength(255)
                    .HasColumnName("contenido");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.HasOne(d => d.CodGrupoNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.CodGrupo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Mensajes_ibfk_2");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Mensajes_ibfk_1");
            });

            modelBuilder.Entity<Retar>(entity =>
            {
                entity.ToTable("Retar");

                entity.HasIndex(e => e.GrupoRetado, "grupo_retado");

                entity.HasIndex(e => e.GrupoRetador, "grupo_retador");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aceptar).HasColumnName("aceptar");

                entity.Property(e => e.GrupoRetado).HasColumnName("grupo_retado");

                entity.Property(e => e.GrupoRetador).HasColumnName("grupo_retador");

                entity.Property(e => e.Mensaje)
                    .HasMaxLength(255)
                    .HasColumnName("mensaje");

                entity.HasOne(d => d.GrupoRetadoNavigation)
                    .WithMany(p => p.RetarGrupoRetadoNavigations)
                    .HasForeignKey(d => d.GrupoRetado)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Retar_ibfk_2");

                entity.HasOne(d => d.GrupoRetadorNavigation)
                    .WithMany(p => p.RetarGrupoRetadorNavigations)
                    .HasForeignKey(d => d.GrupoRetador)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Retar_ibfk_1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PRIMARY");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FotoPerfil)
                    .HasMaxLength(255)
                    .HasColumnName("foto_perfil");

                entity.Property(e => e.JuegoFavorito)
                    .HasMaxLength(100)
                    .HasColumnName("juego_favorito");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Passwd)
                    .HasMaxLength(255)
                    .HasColumnName("passwd");

                entity.Property(e => e.Privacidad)
                    .HasMaxLength(100)
                    .HasColumnName("privacidad");

                entity.Property(e => e.RolApp)
                    .HasMaxLength(100)
                    .HasColumnName("rol_app");
            });

            modelBuilder.Entity<UsuariosEvento>(entity =>
            {
                entity.HasIndex(e => new { e.CodGrupo, e.CodEvento }, "cod_grupo");

                entity.HasIndex(e => e.Username, "username");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aceptar).HasColumnName("aceptar");

                entity.Property(e => e.CodEvento).HasColumnName("cod_evento");

                entity.Property(e => e.CodGrupo).HasColumnName("cod_grupo");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.UsuariosEventos)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UsuariosEventos_ibfk_1");
            });

            modelBuilder.Entity<UsuariosGrupo>(entity =>
            {
                entity.HasIndex(e => e.CodGrupo, "cod_grupo");

                entity.HasIndex(e => e.Username, "username");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodGrupo).HasColumnName("cod_grupo");

                entity.Property(e => e.EsAdmin).HasColumnName("es_admin");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.HasOne(d => d.CodGrupoNavigation)
                    .WithMany(p => p.UsuariosGrupos)
                    .HasForeignKey(d => d.CodGrupo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UsuariosGrupos_ibfk_1");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.UsuariosGrupos)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UsuariosGrupos_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
