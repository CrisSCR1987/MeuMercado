using MeuMercado.Data;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MeuMercado.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ProdutoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public List<ProdutoModel> BuscarPorCategoria(string categoria)
        {
            return _bancoContext.Produtos.Where(x => x.Categoria == categoria).ToList();
        }

        public ProdutoModel BuscarPorId(int id)
        {
            ProdutoModel produto = _bancoContext.Produtos.FirstOrDefault(x => x.Id == id);
            if (produto == null)
            {
                throw new Exception("Não foi possível buscar o Produto pelo ID.");
            }
            return produto;
        }

        public List<ProdutoModel> BuscarPorNome(string nome)
        {
            return _bancoContext.Produtos.Where(x => x.Nome == nome).ToList();
        }

        public List<ProdutoModel> BuscarTodos()
        {
            return _bancoContext.Produtos.Include(x => x.Usuario).ToList();
        }

        public List<ProdutoModel> BuscarTodosDoUsuario(int id)
        {
            return _bancoContext.Produtos.Include(x => x.Usuario).Where(x => x.UsuarioId == id).ToList();
        }

        public ProdutoModel Criar(ProdutoModel produto)
        {
            _bancoContext.Produtos.Add(produto);
            _bancoContext.SaveChanges();
            return produto;
        }

        public ProdutoModel Editar(ProdutoModel produto)
        {
            ProdutoModel produtoDB = BuscarPorId(produto.Id);
            if (produtoDB == null)
            {
                throw new Exception("ID do Produto não consta na base de dados para edição.");
            }
            produtoDB.Nome = produto.Nome;
            produtoDB.Categoria = produto.Categoria;
            produtoDB.Valor = produto.Valor;
            produtoDB.Quantidade = produto.Quantidade;
            _bancoContext.Produtos.Update(produtoDB);
            _bancoContext.SaveChanges();
            return produtoDB;
        }

        public ProdutoModel Excluir(int id)
        {
            ProdutoModel produtoDB = BuscarPorId(id);
            if (produtoDB == null)
            {
                throw new Exception("ID do Produto não consta na base de dados para exclusão.");
            }
            _bancoContext.Produtos.Remove(produtoDB);
            _bancoContext.SaveChanges();
            return produtoDB;
        }
    }
}
