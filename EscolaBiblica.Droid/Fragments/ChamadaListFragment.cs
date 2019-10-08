using System.Linq;
using System.Threading;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Repositorios;
using EscolaBiblica.Droid.Activities;
using EscolaBiblica.Droid.Adapters;
using EscolaBiblica.Droid.ViewHolders;

namespace EscolaBiblica.Droid.Fragments
{
    public class ChamadaListFragment : BaseFragment
    {
        private RecyclerAdapter<ClasseDTO, ClasseViewHolder> _adapter;

        public override int LayoutResource => Resource.Layout.list;

        public override void CreateView(View view)
        {
            _adapter = new RecyclerAdapter<ClasseDTO, ClasseViewHolder>(Resource.Layout.classeItem);
            _adapter.ItemClick += (sender, item, position) =>
            {
                var intent = new Intent(Activity, typeof(ChamadaActivity));
                intent.PutExtra("Setor", item.Setor);
                intent.PutExtra("Congregacao", item.Congregacao);
                intent.PutExtra("Id", item.Id);
                intent.PutExtra("Nome", item.Nome);
                StartActivity(intent);
            };

            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.RecyclerView);
            recyclerView.SetAdapter(_adapter);

            var layoutManager = new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.AddItemDecoration(new DividerItemDecoration(recyclerView.Context, layoutManager.Orientation));

            var refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.SwipeRefresher);
            refresher.SetColorSchemeResources(Resource.Color.colorPrimary);
            refresher.Refresh += (sender, e) => CarregarItens(sender as SwipeRefreshLayout);
            refresher.Post(() => CarregarItens(refresher));
        }

        private void CarregarItens(SwipeRefreshLayout refresher)
        {
            refresher.Refreshing = true;

            ThreadPool.QueueUserWorkItem(o =>
            {
                new ClasseRepositorio(App.Instancia.Token)
                .ObterClasses(App.Instancia.UsuarioId)
                .ContinueWith(task =>
                {
                    try
                    {
                        if (task.Exception != null)
                        {
                            Activity.RunOnUiThread(() => Snackbar.Make(ContentLayout, task.Exception.Message, Snackbar.LengthIndefinite)
                                                                 .SetAction(Resource.String.ok, v => { })
                                                                 .Show());
                            return;
                        }

                        if (task.Result != null && task.Result.Any())
                        {
                            Activity.RunOnUiThread(() => _adapter.LoadList(task.Result.ToList()));
                        }
                    }
                    finally
                    {
                        Activity.RunOnUiThread(() => refresher.Refreshing = false);
                    }
                });
            });
        }
    }
}