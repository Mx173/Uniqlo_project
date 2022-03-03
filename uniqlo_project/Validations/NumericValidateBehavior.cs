using System;
using System.Text.RegularExpressions;
using uniqlo_project.Controls;
using Xamarin.Forms;

namespace uniqlo_project.Validations
{
    public class NumericValidateBehavior : Behavior<ImageEntry>
    {
        private Regex _regex;
        private string _lastValidText = default(string);

        private bool _isDouble;
        public bool IsDouble
        {
            get => _isDouble;
            set
            {
                _isDouble = value;
            }
        }

        private int _digitsBeforeComma = 2;
        public int DigitsBeforeComma
        {
            get => _digitsBeforeComma;
            set
            {
                _digitsBeforeComma = value;
            }
        }

        private int _digitsAfterComma = 2;
        public int DigitsAfterComma
        {
            get => _digitsAfterComma;
            set
            {
                _digitsAfterComma = value;
            }
        }

        private int _maxValue = int.MaxValue;
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
            }
        }

        protected override void OnAttachedTo(ImageEntry entry)
        {
            entry.InternalEntry.TextChanged += OnEntryTextChanged;
            entry.InternalEntry.Unfocused += OnEntryFocuseChanged;
            if (IsDouble)
            {
                _regex = new Regex(string.Format(@"(^\d{{1,{0}}})+(\.\d{{0,{1}}})?$", DigitsBeforeComma, DigitsAfterComma));
            }
            else
            {
                _regex = new Regex(@"^\d+$");
            }

            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(ImageEntry entry)
        {
            entry.InternalEntry.TextChanged -= OnEntryTextChanged;
            entry.InternalEntry.Unfocused -= OnEntryFocuseChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryFocuseChanged(object sender, FocusEventArgs e)
        {
            var entry = sender as ExtendedEntry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            bool isValid = _regex.IsMatch(text);

            if (!isValid)
            {
                entry.Text = _lastValidText;
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as ExtendedEntry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            bool isValid = _regex.IsMatch(text);

            if (isValid)
            {
                entry.Text = text;
                _lastValidText = text;
            }
            else
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            if (Convert.ToDouble(entry.Text) > MaxValue)
            {
                entry.Text = MaxValue.ToString();
            }
        }
    }
}
