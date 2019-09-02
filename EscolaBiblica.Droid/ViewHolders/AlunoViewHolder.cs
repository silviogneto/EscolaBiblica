using Android.Views;
using Android.Widget;
using EscolaBiblica.App.Biblioteca.DTO;

namespace EscolaBiblica.Droid.ViewHolders
{
    public class AlunoViewHolder : BaseViewHolder<AlunoDTO>
    {
        public CheckBox Check { get; }
        public TextView TextNome { get; }

        public AlunoViewHolder(View itemView) : base(itemView)
        {
            Check = itemView.FindViewById<CheckBox>(Resource.Id.Check);
            TextNome = itemView.FindViewById<TextView>(Resource.Id.TextNome);
        }

        public override void Load(AlunoDTO item)
        {
            TextNome.Text = item.Nome;
        }
    }
}