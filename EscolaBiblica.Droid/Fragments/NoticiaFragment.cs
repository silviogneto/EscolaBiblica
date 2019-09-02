using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Repositorios;

namespace EscolaBiblica.Droid.Fragments
{
    public class NoticiaFragment : BaseFragment
    {
        private ListView _listAniversariantes;

        public override int LayoutResource => Resource.Layout.noticia;

        public override void CreateView(View view)
        {
            _listAniversariantes = view.FindViewById<ListView>(Resource.Id.ListAniversariantes);

        }

        public override void OnStart()
        {
            ThreadPool.QueueUserWorkItem(async o =>
            {
                var aniversariantes = new List<AlunoDTO>();
                var alunoRespositorio = new AlunoRepositorio(App.Instancia.Token);

                foreach (var setor in App.Instancia.Setores)
                {
                    foreach (var congregacao in setor.Congregacoes)
                    {
                        var itens = await alunoRespositorio.ObterAniversariantes(setor.Numero, congregacao.Id);
                        if (itens.Any())
                        {
                            aniversariantes.AddRange(itens);
                        }
                    }
                }

                Activity.RunOnUiThread(() =>
                {
                    _listAniversariantes.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, aniversariantes.OrderBy(x => x.DataNascimento)
                                                                                                                                              .ThenBy(x => x.Nome)
                                                                                                                                              .Select(x => $"{x.DataNascimento.ToString("dd/MM")} - {x.Nome}")
                                                                                                                                              .ToArray());
                });
            });

            base.OnStart();
        }
    }
}