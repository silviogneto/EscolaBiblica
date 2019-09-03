using Android.App;
using Android.Content.PM;
using Android.Views;
using EscolaBiblica.Droid.Fragments;

namespace EscolaBiblica.Droid.Activities
{
    [Activity(
        ScreenOrientation = ScreenOrientation.Portrait,
        WindowSoftInputMode = SoftInput.StateHidden | SoftInput.AdjustResize)]
    public class AlterarSenhaActivity : BaseActivity
    {
        public override BaseFragment InitFragment() => new AlterarSenhaFragment();
    }
}