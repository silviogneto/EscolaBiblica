using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using EscolaBiblica.Droid.ViewHolders;

namespace EscolaBiblica.Droid.Adapters
{
    public class RecyclerAdapter<TList, TViewHolder> : RecyclerView.Adapter where TViewHolder : BaseViewHolder<TList>
    {
        private readonly int _itemResourceId;
        private List<int> _selectedItens = new List<int>();

        public delegate void ClickEventHandler(object sender, TList item, int position);
        public event ClickEventHandler ItemClick;
        public event ClickEventHandler ItemLongClick;

        public List<TList> List { get; private set; }
        public int SelectedCount => _selectedItens.Count;

        public RecyclerAdapter(int itemResourceId)
        {
            _itemResourceId = itemResourceId;
        }

        public TList this[int index] => List[index];

        public override int ItemCount => List?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = List[position];

            var viewHolder = holder as BaseViewHolder<TList>;
            viewHolder.Load(item);
            viewHolder.OnSelect(_selectedItens.Contains(position));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(_itemResourceId, parent, false);
            var viewHolder = Activator.CreateInstance(typeof(TViewHolder), itemView) as BaseViewHolder<TList>;

            if (ItemClick != null)
                viewHolder.SetClickListener(position => ItemClick(this, List[position], position));

            if (ItemLongClick != null)
                viewHolder.SetLongClickListener(position => ItemLongClick(this, List[position], position));

            return viewHolder;
        }

        public virtual void LoadList(List<TList> list)
        {
            List = new List<TList>();
            List.AddRange(list);

            NotifyDataSetChanged();
        }

        public virtual void Clear()
        {
            List?.Clear();
            NotifyDataSetChanged();
        }
    }
}