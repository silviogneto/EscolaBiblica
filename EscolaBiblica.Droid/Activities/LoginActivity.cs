using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EscolaBiblica.Droid.Activities
{
    [Activity(
        NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LoginActivity : BaseActivity
    {
        private EditText _textCpf;
        private EditText _textSenha;
        private Button _btnLogin;

        public override int LayoutResource => Resource.Layout.login;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _textCpf = FindViewById<EditText>(Resource.Id.TextCpf);
            _textSenha = FindViewById<EditText>(Resource.Id.TextSenha);
            _btnLogin = FindViewById<Button>(Resource.Id.BtnLogin);
            _btnLogin.Click += (sender, e) =>
            {
                EnableCampos(false);

                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            };
        }

        private void EnableCampos(bool enabled)
        {
            _textCpf.Enabled = enabled;
            _textSenha.Enabled = enabled;
            _btnLogin.Enabled = enabled;
        }
    }
}