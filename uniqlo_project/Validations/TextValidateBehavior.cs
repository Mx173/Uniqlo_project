using System;
using System.Text.RegularExpressions;
using uniqlo_project.Controls;
using Xamarin.Forms;

namespace uniqlo_project.Validations
{
    public class TextValidateBehavior : Behavior<ImageEntry>
    {
        private const string TEXT_PATTERN = "^[^\\\\/?%*:|\"<>.;!@]+$";
        private const string EMAIL_PATTERN = "^[^\\\\/?%*:|\"<>!]+$";
        private Regex _regex;

        private bool _IsMailEntry;
        public bool IsMailEntry
        {
            get => _IsMailEntry;
            set
            {
                _IsMailEntry = value;
            }
        }

        protected override void OnAttachedTo(ImageEntry entry)
        {
            entry.InternalEntry.TextChanged += OnEntryTextChanged;
            entry.InternalEntry.Unfocused += OnEntryFocuseChanged;
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

            if (_regex == null)
            {
                string pattern = IsMailEntry ? EMAIL_PATTERN : TEXT_PATTERN;
                _regex = new Regex(pattern);
            }

            var text = entry.Text;
            bool isValid = _regex.IsMatch(text ?? string.Empty);

            if (!isValid)
            {
                entry.Text = string.Empty;
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as ExtendedEntry;
            if (_regex == null)
            {
                string pattern = IsMailEntry ? EMAIL_PATTERN : TEXT_PATTERN;
                _regex = new Regex(pattern);
            }

            var text = entry.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            bool isValid = _regex.IsMatch(text);

            if (isValid)
            {
                entry.Text = text;
            }
            else
            {
                entry.Text = text.Remove(text.Length - 1);
            }
        }
    }
}
