using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace EscolaBiblica.Droid.Fragments
{
    public class AlterarSenhaFragment : EditarFragment
    {
        private EditText _textSenha;
        private EditText _textConfirmacaoSenha;

        public override int LayoutResource => Resource.Layout.alterarSenha;

        public override void CreateView(View view)
        {
            _textSenha = view.FindViewById<EditText>(Resource.Id.TextSenha);
            _textConfirmacaoSenha = view.FindViewById<EditText>(Resource.Id.TextConfirmacaoSenha);

            Salvar += (sender, e) =>
            {

            };

            AposSalvar += (sender, e) => Activity.Finish();
        }

        public override bool ValidarCampos()
        {
            var valido = true;

            if (string.IsNullOrWhiteSpace(_textSenha.Text))
            {
                _textSenha.Error = "Senha deve ser informada";
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(_textConfirmacaoSenha.Text))
            {
                _textConfirmacaoSenha.Error = "Confirmação de senha deve ser informado";
                valido = false;
            }

            if (_textSenha.Text != _textConfirmacaoSenha.Text)
            {
                _textConfirmacaoSenha.Error = "Confirmação de senha não confere";
                valido = false;
            }

            return valido;
        }
    }
}