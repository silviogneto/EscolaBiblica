using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Preferences;
using EscolaBiblica.App.Biblioteca.DTO;
using Newtonsoft.Json;

namespace EscolaBiblica.Droid
{
    public class App
    {
        private static App _instancia;
        public static App Instancia => _instancia ?? (_instancia = new App());

        private ISharedPreferences _sharedPreferences;

        public int UsuarioId => _sharedPreferences.GetInt("UsuarioId", 0);
        public string Token => _sharedPreferences.GetString("Token", "");
        public DateTime TokenExpiracao => Convert.ToDateTime(_sharedPreferences.GetString("TokenExpiracao", ""));
        public string Perfil => _sharedPreferences.GetString("Perfil", "");
        public IEnumerable<SetorDTO> Setores
        {
            get
            {
                var setoresStr = _sharedPreferences.GetString("Setores", null);
                if (setoresStr == null)
                    return null;

                return JsonConvert.DeserializeObject<IEnumerable<SetorDTO>>(setoresStr);
            }
        }

        private App()
        {
            _sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
        }

        public void Login(int usuarioId, string token, DateTime tokenExpiracao, string perfil, IEnumerable<SetorDTO> setores)
        {
            var preferencesEditor = _sharedPreferences.Edit();
            preferencesEditor.PutInt("UsuarioId", usuarioId);
            preferencesEditor.PutString("Token", token);
            preferencesEditor.PutString("TokenExpiracao", tokenExpiracao.ToString());
            preferencesEditor.PutString("Perfil", perfil);

            if (setores != null)
                preferencesEditor.PutString("Setores", JsonConvert.SerializeObject(setores));

            preferencesEditor.Commit();
        }

        public void Logout()
        {
            var preferencesEditor = _sharedPreferences.Edit();
            preferencesEditor.Remove("UsuarioId");
            preferencesEditor.Remove("Token");
            preferencesEditor.Remove("TokenExpiracao");
            preferencesEditor.Commit();
        }
    }
}