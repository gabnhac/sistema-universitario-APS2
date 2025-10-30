using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Universitario.Dominio.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sistema.Universitario.Infra.Configurations
{
    public class DisciplinaConfiguration: IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable(nameof(Disciplina));

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Nome)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
