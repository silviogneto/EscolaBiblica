using System;
using System.Threading;
using Android.OS;
using Android.Views;

namespace EscolaBiblica.Droid.Fragments
{
    public abstract class EditarFragment : BaseFragment
    {
        public event EventHandler Salvar;
        public event EventHandler AposSalvar;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.editar_menu, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.EditarMenuSalvar)
            {
                if (!ValidarCampos())
                    return false;

                if (Salvar != null)
                {
                    CloseSoftInput();

                    var dialog = LoadingDialog();
                    dialog.Show();

                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        try
                        {
                            Salvar(this, new EventArgs());
                            AposSalvar?.Invoke(this, new EventArgs());
                        }
                        finally
                        {
                            Activity.RunOnUiThread(dialog.Cancel);
                        }
                    });
                }
            }

            return base.OnOptionsItemSelected(item);
        }

        public virtual bool ValidarCampos() => true;
    }
}