using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace EscolaBiblica.Droid.Fragments
{
    public class ChamadaFragment : BaseFragment
    {
        private readonly int _classeId;

        public override int LayoutResource => Resource.Layout.chamada;

        public ChamadaFragment(int classeId)
        {
            _classeId = classeId;
        }

        public override void CreateView(View view)
        {

        }
    }
}