using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace uniqlo_project.Controls
{
    public partial class NavigationBar : ContentView
    {
        public NavigationBar()
        {
            InitializeComponent();
            SettingsVisibility = true;
            BackArrowVisibility = true;
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(NavigationBar), default(string), BindingMode.OneWay);
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty BackArrowVisibilityProperty =
            BindableProperty.Create(nameof(BackArrowVisibility), typeof(bool), typeof(NavigationBar), default(string), BindingMode.OneWay);
        public bool BackArrowVisibility
        {
            get { return (bool)GetValue(BackArrowVisibilityProperty); }
            set { SetValue(BackArrowVisibilityProperty, value); }
        }

        public static readonly BindableProperty SettingsVisibilityProperty =
            BindableProperty.Create(nameof(SettingsVisibility), typeof(bool), typeof(NavigationBar), default(string), BindingMode.OneWay);
        public bool SettingsVisibility
        {
            get { return (bool)GetValue(SettingsVisibilityProperty); }
            set { SetValue(SettingsVisibilityProperty, value); }
        }

        public static readonly BindableProperty GoBackCommandProperty = BindableProperty.Create(nameof(GoBackCommand), typeof(ICommand), typeof(NavigationBar), default(ICommand));
        public ICommand GoBackCommand
        {
            get { return (ICommand)GetValue(GoBackCommandProperty); }
            set { SetValue(GoBackCommandProperty, value); }
        }

        public static readonly BindableProperty SettingsCommandProperty = BindableProperty.Create(nameof(SettingsCommand), typeof(ICommand), typeof(NavigationBar), default(ICommand));
        public ICommand SettingsCommand
        {
            get { return (ICommand)GetValue(SettingsCommandProperty); }
            set { SetValue(SettingsCommandProperty, value); }
        }
    }
}
