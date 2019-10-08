using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase
    {
        public UsuarioRepositorio(string token) : base(token)
        {
        }

        public async Task<IEnumerable<ClasseDTO>> ObterPermissoes(int usuarioId)
        {
            var classes = new List<ClasseDTO>();

            var config = CriarRequestConfig();
            config.EndPoint = "Usuarios/{usuarioId}/Classes";
            config.AddParam("{usuarioId}", usuarioId);

            var retorno = await new WebRequestHelper(config).Get<IEnumerable<ClasseDTO>>();
            if (retorno != null && retorno.Any())
            {
                classes.AddRange(retorno);
            }

            return classes;
        }
    }
}
