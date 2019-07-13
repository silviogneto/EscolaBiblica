using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;

namespace EscolaBiblica.Droid.Activities
{
    [Activity(
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : BaseActivity
    {
        public override int LayoutResource => Resource.Layout.main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var bottomNavView = FindViewById<BottomNavigationView>(Resource.Id.BottomNavView);
            bottomNavView.NavigationItemSelected += (sender, e) => DisplayView(e.Item.ItemId);

            bottomNavView.SelectedItemId = Resource.Id.MenuChamada;
            DisplayView(bottomNavView.SelectedItemId);
        }

        private void DisplayView(int itemId)
        {
            switch (itemId)
            {
                case Resource.Id.MenuChamada:
                    //StartFragment(ChamadaListFragment.NewInstance(), Resource.String.chamadas);
                    break;
            }
        }

        private void StartFragment(Android.Support.V4.App.Fragment fragment, int resourceIdSubtitle = 0)
        {
            if (SupportFragmentManager != null && fragment != null)
            {
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