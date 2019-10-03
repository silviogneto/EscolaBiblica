using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface ICoordenadorRepositorio : IRepositorio<Coordenador>
    {
        Coordenador ObterPorUsuario(int usuario);
    }
}
