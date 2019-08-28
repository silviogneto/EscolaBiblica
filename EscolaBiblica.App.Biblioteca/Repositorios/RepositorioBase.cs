using System;
using System.Collections.Generic;
using System.Text;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public abstract class RepositorioBase
    {
        public string Token { get; set; }
        public IEnumerable<SetorDTO> Setores { get; set; }

        public WebRequestConfig CriarRequestConfig()
        {
            var config = new WebRequestConfig("http://10.0.2.2:5000/api");
            config.AddHeader("Authorization", $"Bearer {Token}");
            return config;
        }
    }
}
