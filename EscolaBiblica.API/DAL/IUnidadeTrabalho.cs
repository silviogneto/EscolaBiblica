using System;
using EscolaBiblica.API.DAL.Repositorios;

namespace EscolaBiblica.API.DAL
{
    public interface IUnidadeTrabalho
    {
        ICongregacaoRepositorio CongregacaoRepositorio { get; }
        ISetorRepositorio SetorRepositorio { get; }
        IUsuarioRepositorio UsuarioRepositorio { get; }

        void ExecutarTransacao(Action<IUnidadeTrabalho> action);
        void Salvar();
    }
}
