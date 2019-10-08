using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EscolaBiblica.App.Biblioteca.DTO;
using EscolaBiblica.App.Biblioteca.Repositorios;
using EscolaBiblica.Droid.Adapters;
using EscolaBiblica.Droid.TextWatchers;
using EscolaBiblica.Droid.ViewHolders;

namespace EscolaBiblica.Droid.Fragments
{
    public class ChamadaFragment : EditarFragment
    {
        private readonly int _setor;
        private readonly int _congregacao;
        private readonly int _classeId;
        private readonly string _classeNome;
        private DateTime _dataAtual;

        private TextView _textMes;
        private EditText _textVisitantes;
        private EditText _textBiblias;
        private EditText _textRevistas;
        private EditText _textOferta;
        private CheckBoxAdapter<AlunoDTO, AlunoViewHolder> _adapter;

        public override int LayoutResource => Resource.Layout.chamada;

        public ChamadaFragment(int setor, int congregacao, int classeId, string classeNome)
        {
            _setor = setor;
            _congregacao = congregacao;
            _classeId = classeId;
            _classeNome = classeNome;
            _dataAtual = DateTime.Now.Date;
        }

        public override void CreateView(View view)
        {
            if (!string.IsNullOrWhiteSpace(_classeNome) && BaseActivity != null)
            {
                BaseActivity.SupportActionBar.Subtitle = $"Classe {_classeNome}";
            }

            _textMes = view.FindViewById<TextView>(Resource.Id.TextMes);
            _textVisitantes = view.FindViewById<EditText>(Resource.Id.TextVisitantes);
            _textBiblias = view.FindViewById<EditText>(Resource.Id.TextBiblias);
            _textRevistas = view.FindViewById<EditText>(Resource.Id.TextRevistas);
            _textOferta = view.FindViewById<EditText>(Resource.Id.TextOferta);
            _textOferta.AddTextChangedListener(new MoneyFormattingTextWatcher());

            view.FindViewById<ImageButton>(Resource.Id.BtnPreviusDay).Click += (sender, e) => AtualizarData(data => ProximaData(data, -1));
            view.FindViewById<ImageButton>(Resource.Id.BtnNextDay).Click += (sender, e) => AtualizarData(data => ProximaData(data));

            _adapter = new CheckBoxAdapter<AlunoDTO, AlunoViewHolder>(Resource.Layout.alunoCheckItem);

            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.RecyclerView);
            recyclerView.SetAdapter(_adapter);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false));

            _dataAtual = ProximaData(_dataAtual);

            AtualizarDados(_dataAtual);

            Salvar += (sender, e) =>
            {
                var dto = new ChamadaDTO();

                if (!string.IsNullOrWhiteSpace(_textVisitantes.Text))
                    dto.Visitantes = Convert.ToInt32(_textVisitantes.Text);

                if (!string.IsNullOrWhiteSpace(_textBiblias.Text))
                    dto.Biblias = Convert.ToInt32(_textBiblias.Text);

                if (!string.IsNullOrWhiteSpace(_textRevistas.Text))
                    dto.Revistas = Convert.ToInt32(_textRevistas.Text);

                if (!string.IsNullOrWhiteSpace(_textOferta.Text))
                    dto.Oferta = Convert.ToDecimal(_textOferta.Text);

                var alunosPresentes = _adapter.GetChecked();
                if (alunosPresentes.Any())
                {
                    dto.Presencas = new List<AlunoDTO>(alunosPresentes.Select(x => new AlunoDTO { Id = x.Id, Nome = x.Nome }));
                }

                new ClasseRepositorio(App.Instancia.Token)
                .SalvarChamada(_setor, _congregacao, _classeId, _dataAtual, dto);
            };
        }

        private DateTime ProximaData(DateTime dataReferencia, int incremento = 1)
        {
            var data = dataReferencia.AddDays(incremento);

            while (data.DayOfWeek != DayOfWeek.Sunday)
                data = data.AddDays(incremento);

            return data;
        }

        private void AtualizarData(Func<DateTime, DateTime> fn) => AtualizarDados(fn(_dataAtual));

        private void AtualizarDados(DateTime data)
        {
            _textMes.Text = data.ToString("dd/MM/yyyy");
            _textVisitantes.Text = string.Empty;
            _textBiblias.Text = string.Empty;
            _textRevistas.Text = string.Empty;
            _textOferta.Text = string.Empty;

            _dataAtual = data.Date;

            _adapter.Clear();

            var dialog = LoadingDialog();
            dialog.Show();

            ThreadPool.QueueUserWorkItem(o =>
            {
                new ClasseRepositorio(App.Instancia.Token)
                .ObterChamada(_setor, _congregacao, _classeId, data)
                .ContinueWith(task =>
                {
                    try
                    {
                        if (task.Result != null)
                        {
                            Activity.RunOnUiThread(() =>
                            {
                                if (task.Result.Visitantes.HasValue)
                                    _textVisitantes.Text = task.Result.Visitantes.Value.ToString();

                                if (task.Result.Biblias.HasValue)
                                    _textBiblias.Text = task.Result.Biblias.Value.ToString();

                                if (task.Result.Revistas.HasValue)
                                    _textRevistas.Text = task.Result.Revistas.Value.ToString();

                                if (task.Result.Oferta.HasValue)
                                    _textOferta.Text = task.Result.Oferta.Value.ToString("F2");

                                if (task.Result.Matriculados != null)
                                    _adapter.LoadList(task.Result.Matriculados.OrderBy(x => x.Nome).ToList());

                                if (task.Result.Presencas != null && task.Result.Presencas.Any())
                                    _adapter.SetSelected(task.Result.Presencas.ToList());
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