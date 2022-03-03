using System;
using System.IO;
using System.Net.Http;
using System.Windows.Input;
using uniqlo_project.Enums;
using uniqlo_project.Resources.Strings;
using uniqlo_project.Services.GetUserInfoService;
using uniqlo_project.Services.LocalStorageService;
using uniqlo_project.Services.UpdatePersonalDataService;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using Xamarin.Forms;

namespace uniqlo_project.ViewModels
{
    public class UserProfilePageViewModel : BaseViewModel
    {
        private IUpdatePersonalDataService _updatePersonalDataService;
        private ILocalStorageService _localStorageService;
        private IGetUserInfoService _getUserInfoService;

        private bool _isGenderChanged;

        public UserProfilePageViewModel(INavigationService navigationService, IUpdatePersonalDataService updatePersonalDataService, ILocalStorageService localStorageService, IGetUserInfoService getUserInfoService)
            : base(navigationService)
        {
            _updatePersonalDataService = updatePersonalDataService;
            _localStorageService = localStorageService;
            _getUserInfoService = getUserInfoService;

            Attachment = Constants.AVATAR;
            GetData();
        }

        private async void GetData()
        {
            var info = await _getUserInfoService.GetUserInfo();
            Attachment = info.Data.Avatar_url;
            if (info.Data.First_name != "")
            {
                Name = info.Data.First_name;
            }
            if (info.Data.User_email != "")
            {
                Email = info.Data.User_email;
            }
            if (info.Data.Phone != "")
            {
                Phone = info.Data.Phone;
            }
            if (info.Data.Birth_date != "")
            {
                Date = info.Data.Birth_date;
            }
            if (info.Data.Gender == "female")
            {
                Gender = EGender.Female;
            }
            else
            {
                Gender = EGender.Male;
            }
        }

        private ImageSource _attachment;
        public ImageSource Attachment
        {
            get { return _attachment; }
            set { SetProperty(ref _attachment, value); }
        }
        
        private string _passwordAlert;
        public string PasswordAlert
        {
            get { return _passwordAlert; }
            set { SetProperty(ref _passwordAlert, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private EGender _gender;
        public EGender Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        private string _password;
        public string OldPassword
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _passRepeat;
        public string NewPassword
        {
            get { return _passRepeat; }
            set { SetProperty(ref _passRepeat, value); }
        }

        private string _date = "";
        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        private ICommand _resetPasswordCommand;
        public ICommand ResetPasswordCommand => _resetPasswordCommand ?? (_resetPasswordCommand = new Command(OnResetPasswordCommand));
        public ICommand ChangeInfoCommand => new Command(OnChangeInfoCommand);

        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(OnBackCommand));

        private ICommand _maleClickCommand;
        public ICommand MaleClickCommand => _maleClickCommand ?? (_maleClickCommand = new Command(OnMaleClickCommand));

        private ICommand _femaleClickCommand;
        public ICommand FemaleClickCommand => _femaleClickCommand ?? (_femaleClickCommand = new Command(OnFemaleClickCommand));

        private ICommand _changeAvatarCommand;
        public ICommand ChangeAvatarCommand => _changeAvatarCommand ?? (_changeAvatarCommand = new Command(OnChangeAvatarCommand));

        private void OnMaleClickCommand(object obj)
        {
            Gender = EGender.Male;
            _isGenderChanged = true;
        }

        private void OnFemaleClickCommand(object obj)
        {
            Gender = EGender.Female;
            _isGenderChanged = true;
        }

        private void OnBackCommand(object obj)
        {
            NavigationService.GoBackAsync();
        }

        private async void OnChangeInfoCommand(object obj)
        {
            if ((Name != "" && Name != null) ||
                (Email != "" && Email != null) ||
                (Phone != "" && Phone != null) ||
                (Date != "" && Date != null) || _isGenderChanged)
            {
                string phoneNew = Phone;
                if (Phone != "" && Phone != null)
                {
                    phoneNew = Phone.Replace(')', (char)32).Replace('(', (char)32).Replace(" ", "");
                }
                var responce = await _updatePersonalDataService.UpdatePersonalData(Name, Gender.ToString().ToLower(), Email, phoneNew, Date);
                if (responce.Code == "200")
                {
                    if (responce.Data.Personal_data.Birth_date != Date)
                    {
                        await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, "Invalid date", Strings.Ok);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, Strings.AlertSuccess, Strings.Ok);

                    }
                    _isGenderChanged = false;

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, responce.Message, Strings.Ok);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, Strings.AlertNotFilled, Strings.Ok);
            }
        }

        private async void OnChangeAvatarCommand(object obj)
        {
            MediaFile photo = null;
            //if (status == false)
            //{
                try
                {
                    if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        photo = await CrossMedia.Current.PickPhotoAsync();
                    }
                }
                catch (Exception ex)
                {
                }
            
            if (photo != null)
            {
                Attachment = ImageSource.FromFile(photo.Path);
                
            }
            try
            {
                var upfilebytes = File.ReadAllBytes(photo.Path);
                //ByteArrayContent baContent = new ByteArrayContent(upfilebytes);

                //var qq = photo.GetStream();
                //var www = ReadToEnd(qq);
                //string temp_inBase64 = Convert.ToBase64String(www);

                //var responce = await _updatePersonalDataService.UpdateProfilePhoto(baContent);

                var access_token = _localStorageService.Load(Constants.TOKEN_KEY);

                HttpClient client = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                StringContent strContent = new StringContent(access_token);
                content.Add(baContent, "profile_photo", "avatar.png");
                content.Add(strContent, "access_token");

                //upload MultipartFormDataContent content async and store response in response var
                var response = await client.PostAsync(GetRequestUrl(Constants.BASE_URL, "user/update-profile-photo/"), content);

                //read response result as a string async into json var
                var responsestr = response.Content.ReadAsStringAsync().Result;



                if (response.StatusCode.ToString() == "OK")
                {
                    await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, Strings.AlertSuccess, Strings.Ok);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, response.RequestMessage.ToString(), Strings.Ok);
                }
            }
            catch (Exception ex)
            {

            }
        }

        internal string GetRequestUrl(string host, string resource)
        {
            return $"{host}{resource}";
        }

        private async void OnResetPasswordCommand()
        {
            if (OldPassword != "" && OldPassword != null)
            {
                PasswordAlert = "";
                var responce = await _updatePersonalDataService.ChangePassword(OldPassword, NewPassword);
                if (responce.Code == "200")
                {
                    await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, Strings.AlertSuccess, Strings.Ok);
                    OldPassword = "";
                    NewPassword = "";
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(Strings.AlertTitle, responce.Message, Strings.Ok);
                }
            }
            else
            {
                PasswordAlert = Strings.RequiredPassword;
            }
        }

        public byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}

