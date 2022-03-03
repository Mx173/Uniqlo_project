using System;
using System.Collections.Generic;
using System.Windows.Input;
using uniqlo_project.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace uniqlo_project.ViewModels
{
    public class ChooseMarathonViewModel : BaseViewModel
    {
        public List<Phone> _phones = new List<Phone>
        {
            new Phone {Title="値下げ", Company="ウルトラライトダウンコンパクトジャケット", Price="3,990", ImageSource="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/429456/item/goods_34_429456.jpg?width=1194" },
            new Phone {Title="値下げ", Company="デザインもディテールも素材もアレンジして、着心地の良さもファッション性もアップ", Price="1,290", ImageSource="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/440946/item/goods_01_440946.jpg?width=1194" },
            new Phone {Title="", Company="3Dリブロングカーディガン（長袖）", Price="1,990", ImageSource="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/447453/item/goods_09_447453.jpg?width=1194" },
            new Phone {Title="値下げ", Company="トレンチコート", Price="7,990", ImageSource="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/444021/sub/goods_444021_sub3.jpg?width=1194" },
        };
        public List<Phone> Phones
        {
            get { return _phones; }
            set { SetProperty(ref _phones, value); }
        }
        

        public ChooseMarathonViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            
        }

        private ICommand _resetPasswordCommand;
        public ICommand ChangeAvatarCommand => _resetPasswordCommand ?? (_resetPasswordCommand = new Command(OnResetPasswordCommandAsync));

        private void OnResetPasswordCommandAsync(object obj)
        {
            NavigationService.NavigateAsync(nameof(HomePageView));
        }
    }

    public class Phone
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Price { get; set; }
        public string ImageSource { get; set; }
    }
}
