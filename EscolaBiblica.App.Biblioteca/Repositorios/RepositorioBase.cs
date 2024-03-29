﻿using EscolaBiblica.App.Biblioteca.Web;

namespace EscolaBiblica.App.Biblioteca.Repositorios
{
    public abstract class RepositorioBase
    {
        private readonly string URL = "http://192.168.1.165:5000/api"; //"http://10.0.2.2:5000/api"
        public string Token { get; }

        public RepositorioBase(string token)
        {
            Token = token;
        }

        public WebRequestConfig CriarRequestConfig()
        {
            var config = new WebRequestConfig(URL);
            config.AddHeader("Authorization", $"Bearer {Token}");
            return config;
        }
    }
}
