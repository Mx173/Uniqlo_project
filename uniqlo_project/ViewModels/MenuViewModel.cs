using System;
using System.Collections.Generic;
using System.Windows.Input;
using uniqlo_project.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace uniqlo_project.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public List<Phone> _phones = new List<Phone>
        {
            new Phone {Title="Marinate chicken breasts, then drizzle with a punchy peanut satay sauce for a no-fuss, midweek meal that's high in protein and big on flavour", Company="Chicken satay salad", Price="450", ImageSource="https://images.immediate.co.uk/production/volatile/sites/30/2020/08/chicken-satay-salad-8f5b068.jpg?quality=90&webp=true&resize=440,400" },
            new Phone {Title="Try our spaghetti dinner for two, with king prawns and harissa dressing. It only takes 20 minutes to make and is healthy too – great for a midweek meal.", Company="Prawn & harissa spaghetti", Price="450", ImageSource="https://images.immediate.co.uk/production/volatile/sites/30/2020/08/prawn-harissa-spaghetti-d29786f.jpg?quality=90&webp=true&resize=440,400" },
            new Phone {Title="A delicious, spicy blend packed full of iron and low in fat to boot. It's ready in under half an hour, or can be made in a slow cooker", Company="Spiced carrot & lentil soup", Price="450", ImageSource="https://images.immediate.co.uk/production/volatile/sites/30/2020/08/recipe-image-legacy-id-1074500_11-ee0d41a.jpg?quality=90&webp=true&resize=440,400" },
            new Phone {Title="Make the most of the colour and flavour of beetroot with our easy roasted cod served on a beetroot, new potato and carrot salad with a lovely zingy dressing", Company="Roasted cod with zingy beetroot salad", Price="450", ImageSource="https://images.immediate.co.uk/production/volatile/sites/30/2021/08/Roasted-cod-with-zingy-beetroot-salad-aff170b.jpg?quality=90&webp=true&resize=600,545" }
        };
        public List<Phone> Phones
        {
            get { return _phones; }
            set { SetProperty(ref _phones, value); }
        }

        private ICommand _settingsCommand;
        public ICommand SettingsCommand => _settingsCommand ?? (_settingsCommand = new Command(OnSettingsCommand));

        private void OnSettingsCommand(object obj)
        {
            NavigationService.NavigateAsync(nameof(HomePageView));
        }

        public MenuViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }
    }
}