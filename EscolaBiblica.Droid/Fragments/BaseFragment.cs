using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace EscolaBiblica.Droid.Fragments
{
    public abstract class BaseFragment : Fragment
    {
        public const int AddCode = 1;
        public const int EditCode = 2;

        public virtual int LayoutResource { get; }
        public RelativeLayout ContentLayout { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (LayoutResource == 0)
                return base.OnCreateView(inflater, container, savedInstanceState);

            var view = LayoutInflater.Inflate(LayoutResource, container, false);

            CreateView(view);

            return view;
        }

        public abstract void CreateView(View view);

        public void CloseSoftInput()
        {
            var inputManager = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            if (inputManager != null && Activity.CurrentFocus != null)
                inputManager.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }
    }
}