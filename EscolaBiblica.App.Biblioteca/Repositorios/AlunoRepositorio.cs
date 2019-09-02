using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public class AlunoRepositorio : RepositorioBase
    {
        public AlunoRepositorio(string token) : base(token)
        {
        }

        public async Task<IEnumerable<AlunoDTO>> ObterAniversariantes(int setor, int congregacao)
        {
            var config = CriarRequestConfig();
            config.EndPoint = "Setores/{setor}/Congregacoes/{congregacao}/Alunos/Aniversariantes/Mes/8";
            config.AddParam("{setor}", setor);
            config.AddParam("{congregacao}", congregacao);

            return await new WebRequestHelper(config).Get<IEnumerable<AlunoDTO>>();
        }
    }
}
