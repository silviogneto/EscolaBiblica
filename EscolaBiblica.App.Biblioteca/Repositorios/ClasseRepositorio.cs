using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public class ClasseRepositorio : RepositorioBase
    {
        public async Task<IEnumerable<ClasseDTO>> ObterClasses()
        {
            var classes = new List<ClasseDTO>();
            if (Setores == null)
                return classes;

            var config = CriarRequestConfig();
            config.EndPoint = "Setores/{setor}/Congregacoes/{congregacao}/Classes";

            foreach (var setor in Setores)
            {
                config.AddParam("{setor}", setor.Numero);

                foreach (var congregacao in setor.Congregacoes)
                {
                    config.AddParam("{congregacao}", congregacao.Id);

                    var retorno = await new WebRequestHelper(config).Get<IEnumerable<ClasseDTO>>();
                    if (retorno != null && retorno.Any())
                    {
                        classes.AddRange(retorno);
                    }
                }
            }

            return classes;
        }

    }
}
