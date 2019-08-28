using EscolaBiblica.API.DAL.Modelos;

namespace EscolaBiblica.API.DAL.Repositorios
{
    public interface IProfessorRepositorio : IRepositorio<Professor>
    {
        Professor ObterPorUsuario(int usuario);
    }
}
