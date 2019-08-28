using Android.Views;
using Android.Widget;
using EscolaBiblica.App.Biblioteca.DTO;

namespace EscolaBiblica.Droid.ViewHolders
{
    public class ClasseViewHolder : BaseViewHolder<ClasseDTO>
    {
        public TextView TextNome { get; }

        public ClasseViewHolder(View itemView) : base(itemView)
        {
            TextNome = itemView.FindViewById<TextView>(Resource.Id.TextNome);
        }

        public override void Load(ClasseDTO item)
        {
            TextNome.Text = item.Nome;
        }
    }
}