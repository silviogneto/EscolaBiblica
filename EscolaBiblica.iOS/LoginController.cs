using EscolaBiblica.App.Biblioteca.Repositorios;
using System;
using System.Net;
using UIKit;

namespace EscolaBiblica.iOS
{
    public partial class LoginController : UIViewController
    {
        public event EventHandler LoginComSucesso;

        public LoginController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnLogin.TouchUpInside += (sender, e) =>
            {
                EnableCampos(false);

                var usuario = textUsurio.Text;
                var senha = textSenha.Text;

                new AutenticacaoRepositorio().Autenticar(usuario, senha).ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        var mensagem = $"Erro na requisição: {task.Exception.Message}";

                        if (task.Exception.InnerException is WebException webException &&
                            webException.Status == WebExceptionStatus.ProtocolError &&
                            webException.Response is HttpWebResponse response &&
                            response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            mensagem = "Usuário e/ou Senha incorretos";
                        }

                        InvokeOnMainThread(() =>
                        {
                            EnableCampos(true);

                            var alert = UIAlertController.Create("Escola Bíblica", mensagem, UIAlertControllerStyle.Alert);
                            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                            PresentViewController(alert, true, null);
                        });

                        return;
                    }

                    InvokeOnMainThread(() => LoginComSucesso?.Invoke(this, new EventArgs()));
                });
            };
        }

        private void EnableCampos(bool enabled)
        {
            textUsurio.Enabled = enabled;
            textSenha.Enabled = enabled;
            btnLogin.Enabled = enabled;
        }
    }
}