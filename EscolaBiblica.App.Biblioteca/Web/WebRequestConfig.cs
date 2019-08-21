using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace EscolaBiblica.App.Biblioteca.Web
{
    public class WebRequestConfig
    {
        private Dictionary<string, JToken> _parametros = new Dictionary<string, JToken>();

        public Dictionary<string, object> Headers { get; } = new Dictionary<string, object>();
        public string Servidor { get; set; }
        public string EndPoint { get; set; }

        public void AddParam(string chave, JToken valor)
        {
            if (chave.Contains("{") || chave.Contains("}"))
                chave = chave.Replace("{", "").Replace("}", "");

            if (_parametros.ContainsKey(chave))
                _parametros[chave] = valor;
            else
                _parametros.Add(chave, valor);
        }

        public void AddHeader(string chave, object valor)
        {
            if (Headers.ContainsKey(chave))
                Headers[chave] = valor;
            else
                Headers.Add(chave, valor);
        }

        private string ReplaceParam(string endPoint)
        {
            foreach (var param in _parametros)
                endPoint = endPoint.Replace($"{{{param.Key}}}", param.Value.ToString());

            return endPoint;
        }

        public override string ToString() => $"{Servidor}/{ReplaceParam(EndPoint)}";
    }
}
