using System;
using EscolaBiblica.API.DAL.Repositorios;

namespace EscolaBiblica.API.DAL
{
    public class UnidadeTrabalho : IUnidadeTrabalho, IDisposable
    {
        #region Variaveis && Propriedades

        private readonly EscolaBiblicaContext _context;

        private ICongregacaoRepositorio _congregacaoRepositorio;
        public ICongregacaoRepositorio CongregacaoRepositorio => _congregacaoRepositorio ?? (_congregacaoRepositorio = new CongregacaoRepositorio(_context));

        private ISetorRepositorio _setorRepositorio;
        public ISetorRepositorio SetorRepositorio => _setorRepositorio ?? (_setorRepositorio = new SetorRepositorio(_context));

        private IUsuarioRepositorio _usuarioRepositorio;
        public IUsuarioRepositorio UsuarioRepositorio => _usuarioRepositorio ?? (_usuarioRepositorio = new UsuarioRepositorio(_context));

        #endregion

        public UnidadeTrabalho(EscolaBiblicaContext context)
        {
            _context = context;
        }

        #region Metodos

        public void ExecutarTransacao(Action<IUnidadeTrabalho> action)
        {
            using (var transacao = _context.Database.BeginTransaction())
            {
                try
                {
                    action(this);
                    transacao.Commit();
                }
                catch
                {
                    transacao.Rollback();
                    throw;
                }
            }
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        #endregion

        #region IDisposable

        private bool _disposed = false;

        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        ~UnidadeTrabalho()
        {
            Clear(false);
        }

        #endregion
    }
}
