using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using EscolaBiblica.Droid.Fragments;

namespace EscolaBiblica.Droid.Activities
{
    public abstract class BaseActivity : AppCompatActivity
    {
        public const int AddCode = 1;
        public const int EditCode = 2;

        public virtual int LayoutResource { get; } = Resource.Layout.content;
        public Android.Support.V7.Widget.Toolbar Toolbar { get; private set; }
        public RelativeLayout ContentLayout { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (LayoutResource > 0)
            {
                SetContentView(LayoutResource);

                SupportRequestWindowFeature(WindowCompat.FeatureActionBar);

                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Toolbar);
                if (toolbar != null)
                {
                    SetSupportActionBar(toolbar);
                    SupportActionBar.Title = Resources.GetString(Resource.String.app_name);

                    Toolbar = toolbar;
                }

                ContentLayout = FindViewById<RelativeLayout>(Resource.Id.ContentLayout);

                var fragment = InitFragment();
                if (fragment != null && ContentLayout != null && SupportFragmentManager != null)
                {
                    fragment.ContentLayout = ContentLayout;

                    SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                    SupportActionBar.SetHomeButtonEnabled(true);

                    SupportFragmentManager.BeginTransaction()
                                          .SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentFade)
                                          .Replace(Resource.Id.ContentBody, fragment)
                                          .Commit();
                }
            }
        }

        public void CloseSoftInput()
        {
            var inputManager = (InputMethodManager)GetSystemService(InputMethodService);
            if (inputManager != null && CurrentFocus != null)
                inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public virtual BaseFragment InitFragment() => null;
    }
}