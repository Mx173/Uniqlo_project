using System;
using System.Collections.Generic;
using uniqlo_project.Validations;
using Xamarin.Forms;

namespace uniqlo_project.Controls
{
    public partial class ImageEntry : ContentView
    {
        public ImageEntry()
        {
            InitializeComponent();
            this.seePassword.Command = new Command(SeePasswordCommand);
            MaxLength = 200;

            ExtEntry.TextChanged += ExtEntry_TextChanged;
        }

        private void ExtEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged?.Invoke(sender, e);
        }

        #region -- Public properties --

        

        public ExtendedEntry InternalEntry => ExtEntry;

        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(ImageEntry), default(TextAlignment), BindingMode.TwoWay);
        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(double), typeof(ImageEntry), default(double), BindingMode.TwoWay);
        public double MaxLength
        {
            get { return (double)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ImageEntry), default(string), BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty PlaceholderTextProperty =
            BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(ImageEntry), default(string));

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(ImageEntry), default(Keyboard));

        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(ImageEntry), true);

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        public static readonly BindableProperty IsPasswordEntryProperty =
            BindableProperty.Create(nameof(IsPasswordEntry), typeof(bool), typeof(ImageEntry), default(bool));

        public bool IsPasswordEntry
        {
            get { return (bool)GetValue(IsPasswordEntryProperty); }
            set { SetValue(IsPasswordEntryProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ImageEntry), 14d);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create(nameof(Spacing), typeof(double), typeof(ImageEntry), 7d);

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public static readonly BindableProperty SeparatorColorProperty =
            BindableProperty.Create(nameof(SeparatorColor), typeof(Color), typeof(ImageEntry), default(Color));

        public Color SeparatorColor
        {
            get { return (Color)GetValue(SeparatorColorProperty); }
            set { SetValue(SeparatorColorProperty, value); }
        }

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ImageEntry), default(ImageSource));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly BindableProperty PasswordImageProperty =
            BindableProperty.Create(nameof(PasswordImage), typeof(ImageSource), typeof(ImageEntry), ImageSource.FromFile("pic_gallery"));

        public ImageSource PasswordImage
        {
            get { return (ImageSource)GetValue(PasswordImageProperty); }
            set { SetValue(PasswordImageProperty, value); }
        }

        public static readonly BindableProperty NumericBehaviorProperty =
            BindableProperty.Create(nameof(NumericBehavior), typeof(bool), typeof(ImageEntry));

        public bool NumericBehavior
        {
            get { return (bool)GetValue(NumericBehaviorProperty); }
            set { SetValue(NumericBehaviorProperty, value); }
        }

        public Action<object, TextChangedEventArgs> TextChanged { get; internal set; }
        #endregion

        #region -- Private helpers --

        private void SeePasswordCommand()
        {
            IsPassword = !IsPassword;
            PasswordImage = IsPassword ? "ic_view_off" : "ic_view_on";
        }

        private void NumericBehaviorCommand()
        {
            if (NumericBehavior)
            {
                (ExtEntry as ExtendedEntry).Behaviors.Add(new TextValidateBehavior());
            }
        }

        #endregion
    }
}
