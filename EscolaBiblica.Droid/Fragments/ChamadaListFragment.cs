using System.Linq;
using System.Threading;
using Android.Content;
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

        public static ChamadaListFragment Instancia() => new ChamadaListFragment();

        private ChamadaListFragment()
        {
        }

        public override void CreateView(View view)
        {
            _adapter = new RecyclerAdapter<ClasseDTO, ClasseViewHolder>(Resource.Layout.classeItem);
            _adapter.ItemClick += (sender, item, position) =>
            {
                var intent = new Intent(Activity, typeof(ChamadaActivity));
                intent.PutExtra("Id", item.Id);
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
                new ClasseRepositorio
                {
                    Token = App.Instancia.Token,
                    Setores = App.Instancia.Setores
                }
                .ObterClasses()
                .ContinueWith(task =>
                {
                    try
                    {
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