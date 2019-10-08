using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.Support.V7.Widget;
using Android.Views;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Repositorios;
using EscolaBiblica.Droid.Adapters;
using EscolaBiblica.Droid.ViewHolders;

namespace EscolaBiblica.Droid.Fragments
{
    public class NoticiaFragment : BaseFragment
    {
        private RecyclerAdapter<AlunoDTO, AniversarianteViewHolder> _adapterAniversariantes;

        public override int LayoutResource => Resource.Layout.noticia;

        public override void CreateView(View view)
        {
            _adapterAniversariantes = new RecyclerAdapter<AlunoDTO, AniversarianteViewHolder>(Resource.Layout.simple_list_item);

            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.RecyclerView);
            recyclerView.SetAdapter(_adapterAniversariantes);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false));
        }

        public override void OnStart()
        {
            ThreadPool.QueueUserWorkItem(async o =>
            {
                var aniversariantes = new List<AlunoDTO>();
                var itens = await new AlunoRepositorio(App.Instancia.Token).ObterAniversariantes(App.Instancia.UsuarioId);
                if (itens.Any())
                {
                    aniversariantes.AddRange(itens);
                }

                if (aniversariantes.Any())
                {
                    Activity.RunOnUiThread(() =>
                    {
                        _adapterAniversariantes.LoadList(aniversariantes.OrderBy(x => x.DataNascimento).ThenBy(x => x.Nome).ToList());
                    });
                }
            });

            base.OnStart();
        }
    }
}