using MeuMercado.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuMercado.Data.Map
{
    public class NotaFiscalMap : IEntityTypeConfiguration<NotaFiscalModel>
    {
        public void Configure(EntityTypeBuilder<NotaFiscalModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Usuario);
            builder.HasOne(x => x.Produto);
        }
    }
}
