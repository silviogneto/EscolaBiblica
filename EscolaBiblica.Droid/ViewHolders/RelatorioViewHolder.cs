using Android.Views;
using Android.Widget;
using EscolaBiblica.App.Biblioteca.DTO;

namespace EscolaBiblica.Droid.ViewHolders
{
    public class RelatorioViewHolder : BaseViewHolder<RelatorioSemanaDTO>
    {
        public TextView LblData { get; private set; }
        public TextView TextMatriculados { get; private set; }
        public TextView TextAusentes { get; private set; }
        public TextView TextPresentes { get; private set; }
        public TextView TextVisitantes { get; private set; }
        public TextView TextOferta { get; private set; }

        public RelatorioViewHolder(View itemView) : base(itemView)
        {
            LblData = itemView.FindViewById<TextView>(Resource.Id.LblData);
            TextMatriculados = itemView.FindViewById<TextView>(Resource.Id.TextMatriculados);
            TextAusentes = itemView.FindViewById<TextView>(Resource.Id.TextAusentes);
            TextPresentes = itemView.FindViewById<TextView>(Resource.Id.TextPresentes);
            TextVisitantes = itemView.FindViewById<TextView>(Resource.Id.TextVisitantes);
            TextOferta = itemView.FindViewById<TextView>(Resource.Id.TextOferta);
        }

        public override void Load(RelatorioSemanaDTO item)
        {
            LblData.Text = $"{item.InicioSemana.ToString("dd/MM/yyyy")} à {item.FimSemana.ToString("dd/MM/yyyy")}";
            TextMatriculados.Text = item.Matriculados.ToString();
            TextAusentes.Text = item.Ausentes.ToString();
            TextPresentes.Text = item.Presentes.ToString();
            TextVisitantes.Text = item.Visitantes.ToString();
            TextOferta.Text = item.Oferta.ToString("C2");
        }
    }
}