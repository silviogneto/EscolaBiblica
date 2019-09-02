using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using EscolaBiblica.Droid.Fragments;

namespace EscolaBiblica.Droid.Activities
{
    [Activity(
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        WindowSoftInputMode = SoftInput.StateHidden | SoftInput.AdjustResize)]
    public class ChamadaActivity : BaseActivity
    {
        public override BaseFragment InitFragment() => new ChamadaFragment(Intent.GetIntExtra("Setor", 0),
                                                                           Intent.GetIntExtra("Congregacao", 0),
                                                                           Intent.GetIntExtra("Id", 0));
    }
}