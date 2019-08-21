using System.Linq;
using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(EscolaBiblicaContext context) : base(context) { }

        public Usuario ObterPorLoginSenha(string login, string senha) => DbSet.FirstOrDefault(x => x.Ativo && x.Login == login && x.Senha == senha);
    }
}
