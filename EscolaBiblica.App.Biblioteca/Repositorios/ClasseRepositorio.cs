using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public class ClasseRepositorio : RepositorioBase
    {
        public ClasseRepositorio(string token) : base(token)
        {
        }

        public async Task<IEnumerable<ClasseDTO>> ObterClasses(IEnumerable<SetorDTO> setores)
        {
            var classes = new List<ClasseDTO>();
            if (setores == null)
                return classes;

            var config = CriarRequestConfig();
            config.EndPoint = "Setores/{setor}/Congregacoes/{congregacao}/Classes";

            foreach (var setor in setores)
            {
                config.AddParam("{setor}", setor.Numero);

                foreach (var congregacao in setor.Congregacoes)
                {
                    config.AddParam("{congregacao}", congregacao.Id);

                    var retorno = await new WebRequestHelper(config).Get<IEnumerable<ClasseDTO>>();
                    if (retorno != null && retorno.Any())
                    {
                        foreach (var dto in retorno)
                        {
                            dto.Congregacao = congregacao.Id;
                            dto.Setor = setor.Numero;
                        }

                        classes.AddRange(retorno);
                    }
                }
            }

            return classes;
        }

        public async Task<ChamadaDTO> ObterChamada(int setor, int congregacao, int classe, DateTime data)
        {
            var config = CriarRequestConfig();
            config.EndPoint = "Setores/{setor}/Congregacoes/{congregacao}/Classes/{classe}/Chamadas/{data}";
            config.AddParam("{setor}", setor);
            config.AddParam("{congregacao}", congregacao);
            config.AddParam("{classe}", classe);
            config.AddParam("{data}", data.ToString("yyyy-MM-dd"));

            return await new WebRequestHelper(config).Get<ChamadaDTO>();
        }

        public async void SalvarChamada(int setor, int congregacao, int classe, DateTime data, ChamadaDTO dto)
        {
            var config = CriarRequestConfig();
            config.EndPoint = "Setores/{setor}/Congregacoes/{congregacao}/Classes/{classe}/Chamadas/{data}";
            config.AddParam("{setor}", setor);
            config.AddParam("{congregacao}", congregacao);
            config.AddParam("{classe}", classe);
            config.AddParam("{data}", data.ToString("yyyy-MM-dd"));

            await new WebRequestHelper(config).Post(dto);
        }
    }
}
