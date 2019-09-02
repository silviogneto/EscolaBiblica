using System;
using System.Linq;
using System.Text.RegularExpressions;
using Android.Text;
using Java.Lang;

namespace EscolaBiblica.Droid.TextWatchers
{
    public class MoneyFormattingTextWatcher : Java.Lang.Object, ITextWatcher, INoCopySpan, IDisposable
    {
        private bool _selfChange;

        public void AfterTextChanged(IEditable s)
        {
            if (_selfChange)
                return;

            var formatted = GetFormatted(s);

            _selfChange = true;

            s.Replace(0, s.Length(), formatted, 0, formatted.Length);

            _selfChange = false;
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            if (_selfChange)
                return;
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            if (_selfChange)
                return;
        }

        private string GetFormatted(ICharSequence s)
        {
            var text = new string(s.ToArray());
            if (string.IsNullOrWhiteSpace(text))
                return text;

            try
            {
                var money = decimal.Parse(new Regex("[.,]").Replace(text, ""));
                if (money > 0m)
                {
                    text = (money / 100).ToString("F2");
                }
            }
            catch
            {
                text = string.Empty;
            }

            return text;
        }
    }
}