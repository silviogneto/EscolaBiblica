using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario, int>
    {
        Usuario ObterPorLoginSenha(string login, string senha);
    }
}
