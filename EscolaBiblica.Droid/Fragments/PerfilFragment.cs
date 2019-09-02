using Android.Content;
using Android.Views;
using Android.Widget;
using EscolaBiblica.Droid.Activities;

namespace EscolaBiblica.Droid.Fragments
{
    public class PerfilFragment : BaseFragment
    {
        public override int LayoutResource => Resource.Layout.perfil;

        public override void CreateView(View view)
        {
            view.FindViewById<TextView>(Resource.Id.TextUsuario).Text = App.Instancia.Usuario;
            view.FindViewById<TextView>(Resource.Id.TextNome).Text = App.Instancia.Nome;

            view.FindViewById<Button>(Resource.Id.BtnAlterarSenha).Click += (sender, e) => StartActivity(new Intent(Activity, typeof(AlterarSenhaActivity)));
        }
    }
}