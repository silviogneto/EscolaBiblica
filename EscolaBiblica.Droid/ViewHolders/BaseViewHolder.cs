using System;
using Android.Support.V7.Widget;
using Android.Views;

namespace EscolaBiblica.Droid.ViewHolders
{
    public abstract class BaseViewHolder<T> : RecyclerView.ViewHolder
    {
        private Action<int> _clickListener;
        private Action<int> _longClickListener;

        public BaseViewHolder(View itemView) : base(itemView)
        {
        }

        public void SetClickListener(Action<int> listener)
        {
            _clickListener = listener;
            ItemView.Click += ClickHandler;
        }

        public void SetLongClickListener(Action<int> listener)
        {
            _longClickListener = listener;
            ItemView.LongClick += LongClickHandler;
        }

        public abstract void Load(T item);

        public virtual void OnSelect(bool selected)
        {
            if (selected)
            {

            }
        }

        private void ClickHandler(object sender, EventArgs e) => _clickListener?.Invoke(AdapterPosition);

        private void LongClickHandler(object sender, EventArgs e) => _longClickListener?.Invoke(AdapterPosition);
    }
}