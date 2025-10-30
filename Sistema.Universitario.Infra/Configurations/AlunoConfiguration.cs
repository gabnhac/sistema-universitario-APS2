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
    public class AlunoConfiguration: IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable(nameof(Aluno));

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Email)
                .IsRequired();

            builder.HasIndex(a => a.Matricula)
                .IsUnique();

            builder.Property(a => a.Nome)
                .HasMaxLength(150)
                .IsRequired();

        }
    }
}
