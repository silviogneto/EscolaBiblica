using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public abstract class RepositorioBase
    {
        public string Token { get; }

        public RepositorioBase(string token)
        {
            Token = token;
        }

        public WebRequestConfig CriarRequestConfig()
        {
            var config = new WebRequestConfig("http://10.0.2.2:5000/api");
            config.AddHeader("Authorization", $"Bearer {Token}");
            return config;
        }
    }
}
