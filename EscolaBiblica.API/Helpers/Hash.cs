using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EscolaBiblica.API.Helpers
{
    public static class Hash
    {
        // TODO: Verificar jeito de buscar a chave do arquivo appsettings.json
        private const string Key = "Ch@ve$uper$3cretaDo@PP";

        public static string GerarHash(string str)
        {
            var salt = Encoding.UTF8.GetBytes(Key);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(str, salt, KeyDerivationPrf.HMACSHA256, 10000, salt.Length));
        }
    }
}
