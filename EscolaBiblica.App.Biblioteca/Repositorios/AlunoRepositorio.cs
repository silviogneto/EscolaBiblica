using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<AlunoDTO>> ObterAniversariantes(int usuarioId)
        {
            var aniversariantes = new List<AlunoDTO>();
            var classes = await new ClasseRepositorio(Token).ObterClasses(usuarioId);
            if (!classes.Any())
                return aniversariantes;

            foreach (var setores in classes.GroupBy(x => x.Setor))
            {
                foreach (var congregacoes in setores.GroupBy(x => x.Congregacao))
                {
                    var config = CriarRequestConfig();
                    config.EndPoint = "Setores/{setor}/Congregacoes/{congregacao}/Alunos/Aniversariantes/Mes";
                    config.AddParam("{setor}", setores.Key);
                    config.AddParam("{congregacao}", congregacoes.Key);

                    aniversariantes.AddRange(await new WebRequestHelper(config).Get<IEnumerable<AlunoDTO>>());
                }
            }

            return aniversariantes;
        }
    }
}
