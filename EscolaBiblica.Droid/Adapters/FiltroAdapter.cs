using System;
using System.Collections.Generic;
using System.Linq;
using EscolaBiblica.Droid.ViewHolders;

namespace EscolaBiblica.Droid.Adapters
{
    public class FiltroAdapter<TList, TViewHolder> : RecyclerAdapter<TList, TViewHolder> where TViewHolder : BaseViewHolder<TList>
    {
        private List<TList> _todosItens;

        public FiltroAdapter(int itemResourceId) : base(itemResourceId)
        {
        }

        public void Filtrar(Func<TList, bool> filtroFn)
        {
            if (_todosItens == null)
                _todosItens = List;

            if (_todosItens != null)
                LoadList(_todosItens.Where(filtroFn).ToList());
        }
    }
}