using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace EscolaBiblica.Droid.Activities
{
    [Activity(
        NoHistory = true,
        MainLauncher = true,
        Theme = "@style/AppTheme.SplashScreen",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashScreenActivity : BaseActivity
    {
        public override int LayoutResource => 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var worker = new Task(StartApp);
            worker.ContinueWith(task =>
            {
                StartActivity(new Intent(Application.Context, typeof(LoginActivity)));
                Finish();
            }, TaskScheduler.FromCurrentSynchronizationContext());
            worker.Start();
        }

        private void StartApp()
        {
        }
    }
}