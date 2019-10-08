using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using EscolaBiblica.App.Biblioteca.Helpers;
using EscolaBiblica.Droid.Extensions;
using EscolaBiblica.Droid.Fragments;

namespace EscolaBiblica.Droid.Activities
{
    [Activity(
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {
        public override int LayoutResource => Resource.Layout.main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var bottomNavView = FindViewById<BottomNavigationView>(Resource.Id.BottomNavView);
            bottomNavView.SetShiftMode(false, false);
            bottomNavView.NavigationItemSelected += (sender, e) => DisplayView(e.Item.ItemId);

            if (App.Instancia.Perfil == Perfil.Professor)
            {
                bottomNavView.Menu.RemoveItem(Resource.Id.MenuRelatorio);
            }

            bottomNavView.SelectedItemId = Resource.Id.MenuChamada;
            DisplayView(bottomNavView.SelectedItemId);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.MainMenuLogout)
            {
                App.Instancia.Logout();

                var tela = new Intent(Application.Context, typeof(LoginActivity));
                tela.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                StartActivity(tela);
                Finish();
            }

            return base.OnOptionsItemSelected(item);
        }

        private void DisplayView(int itemId)
        {
            switch (itemId)
            {
                case Resource.Id.MenuNoticias:
                    StartFragment(new NoticiaFragment());
                    break;
                case Resource.Id.MenuRelatorio:
                    StartFragment(new RelatorioFragment());
                    break;
                case Resource.Id.MenuChamada:
                    StartFragment(new ChamadaListFragment());
                    break;
                case Resource.Id.MenuPerfil:
                    StartFragment(new PerfilFragment());
                    break;
            }
        }

        private void StartFragment(BaseFragment fragment, int resourceIdSubtitle = 0)
        {
            if (SupportFragmentManager != null && fragment != null)
            {
                fragment.ContentLayout = ContentLayout;

                SupportFragmentManager.BeginTransaction()
                                      .SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentFade)
                                      .Replace(Resource.Id.ContentBody, fragment)
                                      .Commit();

                if (resourceIdSubtitle > 0)
                    SupportActionBar.Subtitle = Resources.GetString(resourceIdSubtitle);
            }
        }
    }
}