using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public class RelatorioRepositorio : RepositorioBase
    {
        public RelatorioRepositorio(string token) : base(token)
        {
        }

        public async Task<RelatorioDTO> ObterRelatorio(int setor, int congregacao, int ano, int mes)
        {
            var config = CriarRequestConfig();
            config.EndPoint = "Setores/{setor}/Congregacoes/{congregacao}/Relatorios/Ano/{ano}/Mes/{mes}";
            config.AddParam("{setor}", setor);
            config.AddParam("{congregacao}", congregacao);
            config.AddParam("{ano}", ano);
            config.AddParam("{mes}", mes);

            return await new WebRequestHelper(config).Get<RelatorioDTO>();
        }
    }
}
