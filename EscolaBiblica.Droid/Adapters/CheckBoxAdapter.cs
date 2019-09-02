using System.Collections.Generic;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EscolaBiblica.Droid.ViewHolders;

namespace EscolaBiblica.Droid.Adapters
{
    public class CheckBoxAdapter<TList, TViewHolder> : FiltroAdapter<TList, TViewHolder> where TViewHolder : BaseViewHolder<TList>
    {
        private HashSet<int> _checkedItens = new HashSet<int>();

        public CheckBoxAdapter(int itemResourceId) : base(itemResourceId)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);

            var viewHolder = holder as BaseViewHolder<TList>;
            var check = viewHolder.ItemView.FindViewById<CheckBox>(Resource.Id.Check);
            if (check != null)
            {
                check.CheckedChange += delegate { OnItemClick(check, position); };
                check.Checked = _checkedItens.Contains(position);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var viewHolder = base.OnCreateViewHolder(parent, viewType) as BaseViewHolder<TList>;
            var check = viewHolder.ItemView.FindViewById<CheckBox>(Resource.Id.Check);
            if (check != null)
            {
                viewHolder.SetClickListener(position =>
                {
                    check.Checked = !check.Checked;

                    OnItemClick(check, position);
                });
            }

            return viewHolder;
        }

        public IEnumerable<TList> GetChecked()
        {
            foreach (var i in _checkedItens)
                yield return this[i];
        }

        public void SetSelected(IEnumerable<TList> selected)
        {
            for (int i = 0, length = ItemCount; i < length; i++)
            {
                var item = this[i];

                if (selected.Any(x => x.Equals(item)))
                    _checkedItens.Add(i);
            }

            NotifyDataSetChanged();
        }

        public void ClearAll()
        {
            List?.Clear();
            ClearChecked();
        }

        public void ClearChecked()
        {
            _checkedItens.Clear();
            NotifyDataSetChanged();
        }

        private void OnItemClick(CheckBox check, int position)
        {
            if (check.Checked)
            {
                _checkedItens.Add(position);
            }
            else
            {
                _checkedItens.Remove(position);
            }
        }
    }
}