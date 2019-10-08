using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ClasseDTO>> ObterClasses(int usuarioId)
        {
            var config = CriarRequestConfig();
            config.EndPoint = "Usuarios/{usuarioId}/Classes";
            config.AddParam("{usuarioId}", usuarioId);

            return await new WebRequestHelper(config).Get<IEnumerable<ClasseDTO>>();
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

        public async Task<IEnumerable<SetorDTO>> ObterCongregacoes(int usuarioId)
        {
            var config = CriarRequestConfig();
            config.EndPoint = "Usuarios/{usuarioId}/Congregacoes";
            config.AddParam("{usuarioId}", usuarioId);

            return await new WebRequestHelper(config).Get<IEnumerable<SetorDTO>>();
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
