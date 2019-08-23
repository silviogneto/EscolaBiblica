using System;
using EscolaBiblica.API.DAL.Repositorios;

namespace EscolaBiblica.API.DAL
{
    public interface IUnidadeTrabalho
    {
        IAlunoRepositorio AlunoRepositorio { get; }
        IChamadaRepositorio ChamadaRepositorio { get; }
        IClasseRepositorio ClasseRepositorio { get; }
        ICongregacaoRepositorio CongregacaoRepositorio { get; }
        IMatriculaRepositorio MatriculaRepositorio { get; }
        IProfessorRepositorio ProfessorRepositorio { get; }
        ISetorRepositorio SetorRepositorio { get; }
        IUsuarioRepositorio UsuarioRepositorio { get; }

        void ExecutarTransacao(Action<IUnidadeTrabalho> action);
        void Salvar();
    }
}
