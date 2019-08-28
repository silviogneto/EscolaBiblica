using System.Net;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Repositorios;

namespace EscolaBiblica.Droid.Activities
{
    [Activity(
        NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        WindowSoftInputMode = SoftInput.StateHidden | SoftInput.AdjustResize)]
    public class LoginActivity : BaseActivity
    {
        private EditText _textUsuario;
        private EditText _textSenha;
        private Button _btnLogin;

        public override int LayoutResource => Resource.Layout.login;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _textUsuario = FindViewById<EditText>(Resource.Id.TextUsuario);
            _textSenha = FindViewById<EditText>(Resource.Id.TextSenha);
            _btnLogin = FindViewById<Button>(Resource.Id.BtnLogin);
            _btnLogin.Click += async (sender, e) =>
            {
                EnableCampos(false);

                try
                {
                    var result = await new AutenticacaoRepositorio().Autenticar(_textUsuario.Text, _textSenha.Text);
                    GravarDadosUsuario(result);

                    StartActivity(new Intent(this, typeof(MainActivity)));
                    Finish();
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    EnableCampos(true);

                    if (ex.Response is HttpWebResponse response &&
                        response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Snackbar.Make(FindViewById<LinearLayout>(Resource.Id.ContentLayout), Resource.String.erroNaoAutorizado, Snackbar.LengthIndefinite)
                                .SetAction(Resource.String.ok, v => { })
                                .Show();
                    }
                }
                catch
                {
                    EnableCampos(true);

                    Snackbar.Make(FindViewById<LinearLayout>(Resource.Id.ContentLayout), Resource.String.erroRequisicao, Snackbar.LengthIndefinite)
                            .SetAction(Resource.String.ok, v => { })
                            .Show();
                }
            };
        }

        private void GravarDadosUsuario(UsuarioDTO usuarioDto)
        {
            App.Instancia.Login(usuarioDto.Id, usuarioDto.Token, usuarioDto.Expires, usuarioDto.Perfil, usuarioDto.Setores);
        }

        private void EnableCampos(bool enabled)
        {
            _textUsuario.Enabled = enabled;
            _textSenha.Enabled = enabled;
            _btnLogin.Enabled = enabled;
        }
    }
}