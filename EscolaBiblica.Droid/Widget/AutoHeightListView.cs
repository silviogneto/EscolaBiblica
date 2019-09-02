using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace EscolaBiblica.Droid.Widget
{
    [Register("EscolaBiblica.Droid.Widget.AutoHeightListView")]
    public class AutoHeightListView : ListView
    {
        public AutoHeightListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        protected override void OnDraw(Canvas canvas)
        {
            var heightItem = (GetChildAt(0)?.Height ?? 0) + 1;
            var height = heightItem * Count;

            if (LayoutParameters.Height != height)
            {
                var param = LayoutParameters;
                param.Height = height;
                LayoutParameters = param;
            }

            base.OnDraw(canvas);
        }
    }
}