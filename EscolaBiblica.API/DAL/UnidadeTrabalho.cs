using System;
using EscolaBiblica.API.DAL.Repositorios;

namespace EscolaBiblica.API.DAL
{
    public class UnidadeTrabalho : IUnidadeTrabalho, IDisposable
    {
        #region Variaveis && Propriedades

        private readonly EscolaBiblicaContext _context;

        private IAlunoRepositorio _alunoRepositorio;
        public IAlunoRepositorio AlunoRepositorio => _alunoRepositorio ?? (_alunoRepositorio = new AlunoRepositorio(_context));

        private IChamadaRepositorio _chamadaRepositorio;
        public IChamadaRepositorio ChamadaRepositorio => _chamadaRepositorio ?? (_chamadaRepositorio = new ChamadaRepositorio(_context));

        private IClasseRepositorio _classeRepositorio;
        public IClasseRepositorio ClasseRepositorio => _classeRepositorio ?? (_classeRepositorio = new ClasseRepositorio(_context));

        private ICongregacaoRepositorio _congregacaoRepositorio;
        public ICongregacaoRepositorio CongregacaoRepositorio => _congregacaoRepositorio ?? (_congregacaoRepositorio = new CongregacaoRepositorio(_context));

        private ICoordenadorRepositorio _coordenadorRepositorio;
        public ICoordenadorRepositorio CoordenadorRepositorio => _coordenadorRepositorio ?? (_coordenadorRepositorio = new CoordenadorRepositorio(_context));

        private ICoordenadorCongregacaoRepositorio _coordenadorCongregacaoRepositorio;
        public ICoordenadorCongregacaoRepositorio CoordenadorCongregacaoRepositorio => _coordenadorCongregacaoRepositorio ?? (_coordenadorCongregacaoRepositorio = new CoordenadorCongregacaoRepositorio(_context));

        private IMatriculaRepositorio _matriculaRepositorio;
        public IMatriculaRepositorio MatriculaRepositorio => _matriculaRepositorio ?? (_matriculaRepositorio = new MatriculaRepositorio(_context));

        private IProfessorClasseRepositorio _professorClasseRepositorio;
        public IProfessorClasseRepositorio ProfessorClasseRepositorio => _professorClasseRepositorio ?? (_professorClasseRepositorio = new ProfessorClasseRepositorio(_context));

        private IProfessorRepositorio _professorRepositorio;
        public IProfessorRepositorio ProfessorRepositorio => _professorRepositorio ?? (_professorRepositorio = new ProfessorRepositorio(_context));

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
                    Salvar();
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
