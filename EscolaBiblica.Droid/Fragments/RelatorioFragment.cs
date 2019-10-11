using System;
using System.Linq;
using System.Threading;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Repositorios;
using EscolaBiblica.Droid.Adapters;
using EscolaBiblica.Droid.ViewHolders;

namespace EscolaBiblica.Droid.Fragments
{
    public class RelatorioFragment : BaseFragment
    {
        private DateTime _dataAtual;

        private Spinner _comboSetor;
        private ArrayAdapter<SetorDTO> _adapterSetor;
        private Spinner _comboCongregacao;
        private ArrayAdapter<CongregacaoDTO> _adapterCongregacao;
        private TextView _textMes;
        private TextView _textSetor;
        private TextView _textCongregacao;
        private TextView _textOfertaMes;
        private TextView _textOfertaDepartamento;
        private RecyclerAdapter<RelatorioSemanaDTO, RelatorioViewHolder> _adapter;

        public override int LayoutResource => Resource.Layout.relatorio;

        public override void CreateView(View view)
        {
            _textMes = view.FindViewById<TextView>(Resource.Id.TextMes);
            view.FindViewById<ImageButton>(Resource.Id.BtnPreviusDay).Click += (sender, e) => AtualizarData(data => new DateTime(data.Year, data.Month, 1).AddMonths(-1));
            view.FindViewById<ImageButton>(Resource.Id.BtnNextDay).Click += (sender, e) => AtualizarData(data => new DateTime(data.Year, data.Month, 1).AddMonths(1));

            _textSetor = view.FindViewById<TextView>(Resource.Id.TextSetor);
            _textCongregacao = view.FindViewById<TextView>(Resource.Id.TextCongregacao);
            _textOfertaMes = view.FindViewById<TextView>(Resource.Id.TextOfertaMes);
            _textOfertaDepartamento = view.FindViewById<TextView>(Resource.Id.TextOfertaDepartamento);

            _adapter = new RecyclerAdapter<RelatorioSemanaDTO, RelatorioViewHolder>(Resource.Layout.relatorio_semana);

            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.RecyclerView);
            recyclerView.SetAdapter(_adapter);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false));

            _dataAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            _comboSetor = view.FindViewById<Spinner>(Resource.Id.ComboSetor);
            _comboCongregacao = view.FindViewById<Spinner>(Resource.Id.ComboCongregacao);
            _comboCongregacao.ItemSelected += (sender, e) => AtualizarDados(_dataAtual);
            _comboCongregacao.Post(async () =>
            {
                var setores = await new ClasseRepositorio(App.Instancia.Token).ObterCongregacoes(App.Instancia.UsuarioId);
                _adapterSetor = new ArrayAdapter<SetorDTO>(Activity, Resource.Layout.combo_item, setores.ToList());
                _comboSetor.Visibility = setores.Count() == 1 ? ViewStates.Gone : ViewStates.Visible;
                _comboSetor.SetSelection(0);

                if (setores.Count() > 1)
                {
                    _adapterSetor.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    _comboSetor.Adapter = _adapterSetor;
                    _comboSetor.ItemSelected += (sender, e) =>
                    {
                        var setor = _adapterSetor.GetItem(e.Position);

                        _adapterCongregacao = new ArrayAdapter<CongregacaoDTO>(Activity, Resource.Layout.combo_item, setor.Congregacoes.ToList());
                        _adapterCongregacao.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                        _comboCongregacao.Adapter = _adapterCongregacao;
                    };
                }

                var congregacoes = setores.First().Congregacoes.ToList();
                _adapterCongregacao = new ArrayAdapter<CongregacaoDTO>(Activity, Resource.Layout.combo_item, congregacoes);
                _comboCongregacao.SetSelection(0);

                if (setores.Count() == 1 && congregacoes.Count == 1)
                {
                    view.FindViewById<RelativeLayout>(Resource.Id.PanelSetor).Visibility = ViewStates.Gone;
                }
                else
                {
                    _adapterCongregacao.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    _comboCongregacao.Adapter = _adapterCongregacao;
                }

                AtualizarDados(_dataAtual);
            });
        }

        private void AtualizarData(Func<DateTime, DateTime> fn) => AtualizarDados(fn(_dataAtual));

        private void AtualizarDados(DateTime data)
        {
            _textMes.Text = data.ToString("MM/yyyy");

            _dataAtual = new DateTime(data.Year, data.Month, 1);

            _adapter.Clear();

            var setor = _adapterSetor.GetItem(_comboSetor.SelectedItemPosition).Numero;
            var congregacao = _adapterCongregacao.GetItem(_comboCongregacao.SelectedItemPosition).Id;

            var dialog = LoadingDialog();
            dialog.Show();

            ThreadPool.QueueUserWorkItem(o =>
            {
                new RelatorioRepositorio(App.Instancia.Token)
                .ObterRelatorio(setor, congregacao, _dataAtual.Year, _dataAtual.Month)
                .ContinueWith(task =>
                {
                    try
                    {
                        if (task.Exception != null)
                        {
                            Activity.RunOnUiThread(() =>
                            {
                                _textSetor.Text = "-";
                                _textCongregacao.Text = "-";
                                _textOfertaMes.Text = "-";
                                _textOfertaDepartamento.Text = "-";

                                Snackbar.Make(ContentLayout, task.Exception.Message, Snackbar.LengthIndefinite)
                                                                     .SetAction(Resource.String.ok, v => { })
                                                                     .Show();
                            });

                            return;
                        }

                        if (task.Result != null)
                        {
                            Activity.RunOnUiThread(() =>
                            {
                                _textSetor.Text = task.Result.Setor.ToString("00");
                                _textCongregacao.Text = task.Result.NomeCongregacao;
                                _textOfertaMes.Text = task.Result.OfertaMes.ToString("C2");
                                _textOfertaDepartamento.Text = task.Result.OfertaDepartamento.ToString("C2");

                                _adapter.LoadList(task.Result.Semanas.ToList());
                            });
                        }
                    }
                    finally
                    {
                        Activity.RunOnUiThread(dialog.Cancel);
                    }
                });
            });
        }
    }
}