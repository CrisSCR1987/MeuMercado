using MeuMercado.Data;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuMercado.Repositorio
{
    public class NotaFiscalRepositorio : INotaFiscalRepositorio
    {
        private readonly BancoContext _bancoContext;

        public NotaFiscalRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public NotaFiscalModel BuscarPorId(int id)
        {
            return _bancoContext.NotasFiscais.FirstOrDefault(x => x.Id == id);
        }

        public List<NotaFiscalModel> BuscarTodosDoUsuario(int usuarioId)
        {
            return _bancoContext.NotasFiscais.Include(x => x.Usuario).Include(x => x.Produto).Where(x => x.UsuarioId == usuarioId).ToList();
        }
        public NotaFiscalModel Criar(NotaFiscalModel notaFiscal)
        {
            if (notaFiscal == null)
            {
                throw new Exception("Não é possível criar uma Nota Fiscal nula.");
            }
            _bancoContext.NotasFiscais.Add(notaFiscal);
            _bancoContext.SaveChanges();
            return notaFiscal;
        }
    }
}
