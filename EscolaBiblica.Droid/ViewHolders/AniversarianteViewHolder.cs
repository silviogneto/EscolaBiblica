using Android.Views;
using Android.Widget;
using EscolaBiblica.App.Biblioteca.DTO;

namespace EscolaBiblica.Droid.ViewHolders
{
    public class AniversarianteViewHolder : BaseViewHolder<AlunoDTO>
    {
        public TextView Texto { get; private set; }

        public AniversarianteViewHolder(View itemView) : base(itemView)
        {
            Texto = itemView.FindViewById<TextView>(Resource.Id.Texto);
        }

        public override void Load(AlunoDTO item)
        {
            Texto.Text = $"{item.DataNascimento.ToString("dd/MM")} - {item.Nome}";
        }
    }
}