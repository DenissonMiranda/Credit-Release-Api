using CreditRelease.Domain.Entity;
using CreditRelease.Domain.Interfaces;
using CreditRelease.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CreditRelease.Infra.Data.Repository
{
    public abstract class RepositoryBase<TEntidade> : IRepositoryBase<TEntidade>
       where TEntidade : EntityBase
    {
        protected readonly Context _context;

        public RepositoryBase(Context context)
        {
            _context = context;
        }

        public async Task<decimal> Incluir(TEntidade entidade)
        {
            await _context.IniTransacao();
            var id = _context.Set<TEntidade>().Add(entidade).Entity;
            await _context.SendChanges();

            return 1;
        }

        public async Task<TEntidade> IncluirComRetorno(TEntidade entidade)
        {
            await _context.IniTransacao();
            var l_entidade = _context.Set<TEntidade>().Add(entidade).Entity;
            await _context.SendChanges();

            return l_entidade;
        }

        public async Task Excluir(TEntidade entidade)
        {
            await _context.IniTransacao();
            _context.Set<TEntidade>().Remove(entidade);
            await _context.SendChanges();
        }

        public async Task Alterar(TEntidade entidade)
        {
            await _context.IniTransacao();
            _context.Set<TEntidade>().Update(entidade);
            await _context.SendChanges();
        }

        public async Task<TEntidade> SelecionarPorId(long id)
        {
            return await _context.Set<TEntidade>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntidade>> SelecionarTodos()
        {
            return await _context.Set<TEntidade>().ToListAsync();
        }
    }
}
