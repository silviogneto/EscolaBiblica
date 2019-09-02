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
        public string Usuario => _sharedPreferences.GetString("Login", string.Empty);
        public string Nome => _sharedPreferences.GetString("Nome", string.Empty);
        public string Token => _sharedPreferences.GetString("Token", string.Empty);
        public DateTime? TokenExpiracao
        {
            get
            {
                var data = _sharedPreferences.GetString("TokenExpiracao", string.Empty);
                if (string.IsNullOrWhiteSpace(data))
                    return null;

                return Convert.ToDateTime(data);
            }
        }
        public string Perfil => _sharedPreferences.GetString("Perfil", string.Empty);
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

        public void Login(UsuarioDTO usuarioDTO)
        {
            var preferencesEditor = _sharedPreferences.Edit();
            preferencesEditor.PutInt("UsuarioId", usuarioDTO.Id);
            preferencesEditor.PutString("Login", usuarioDTO.Login);
            preferencesEditor.PutString("Nome", usuarioDTO.Nome);
            preferencesEditor.PutString("Token", usuarioDTO.Token);
            preferencesEditor.PutString("TokenExpiracao", usuarioDTO.Expires.ToString());
            preferencesEditor.PutString("Perfil", usuarioDTO.Perfil);

            if (usuarioDTO.Setores != null)
                preferencesEditor.PutString("Setores", JsonConvert.SerializeObject(usuarioDTO.Setores));

            preferencesEditor.Commit();
        }

        public void Logout()
        {
            var preferencesEditor = _sharedPreferences.Edit();
            preferencesEditor.Remove("UsuarioId");
            preferencesEditor.Remove("Login");
            preferencesEditor.Remove("Nome");
            preferencesEditor.Remove("Token");
            preferencesEditor.Remove("TokenExpiracao");
            preferencesEditor.Remove("Perfil");
            preferencesEditor.Remove("Setores");
            preferencesEditor.Commit();
        }
    }
}