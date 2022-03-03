using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace uniqlo_project.Controls
{
    public class UnselectedList : ListView
    {
        public UnselectedList()
        {
            this.ItemTapped += (sender, e) =>
            {
                if (TappedCommand != null)
                {
                    TappedCommand?.Execute(e.Item);
                }

                SelectedItem = null;
            };

            this.ItemAppearing += (sender, e) =>
            {
                if (ItemAppearingCommand != null)
                {
                    ItemAppearingCommand?.Execute(e.Item);
                }
            };
        }

        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(UnselectedList), default(ICommand));

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public static readonly BindableProperty ItemAppearingCommandProperty =
            BindableProperty.Create(nameof(ItemAppearingCommand), typeof(ICommand), typeof(UnselectedList), default(ICommand));

        public ICommand ItemAppearingCommand
        {
            get { return (ICommand)GetValue(ItemAppearingCommandProperty); }
            set { SetValue(ItemAppearingCommandProperty, value); }
        }
    }
}
