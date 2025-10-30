using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Universitario.Dominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Universitario.Infra.Configurations
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable(nameof(Curso));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasMany(c => c.Turmas)
                .WithOne(t => t.Curso)
                .HasForeignKey(t => t.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Disciplinas)
                .WithMany(d => d.Cursos);

        }
    }
}
