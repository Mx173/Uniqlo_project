using System;
using System.Collections.Generic;
using uniqlo_project.Models;
using Xamarin.Forms;
using static uniqlo_project.Models.GetUserInfoResponse;

namespace uniqlo_project
{
    public static class Constants
    {
        public const string BASE_URL = "url";
        public const string APP_KEY = "W58o1j9qDk-JBg8RNdH8p-XjPjnG96K6-pLMX7DPGel";

        public const double DELTA_SCALE_COEF = 0.001;

        public const int COMMAND_DELAY_TIME = 400;

        public const double MIN_SCALE_COEF = 0.97;

        public const double NORMAL_SCALE_COEF = 1;

        public const int LIST_TAKE_COUNT = 10;

        public const string NavigationParameterKey = "NavigationParameterKey";
        public static string IS_AUTOLOGIN_ALLOWED = "IS_AUTOLOGIN_ALLOWED";
        public static ImageSource AVATAR;
        public static string LOGIN;
        public const string TOKEN_KEY = "TOKEN_KEY";

        public static class UserValidation
        {
            public const string EMAIL_PATTERN = @".+\@.+";

            public const string PASSWORD_PATTERN = @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$";

            public const string NAME_PATTERN = @"^[\p{L}\s]+$";

            
            //public const string PHONE_PATTERN = @"^[0-9]+$";

            //public const string VERIFICATION_CODE_PATTERN = @"^[0-9]{6,6}$";

            //public const string ID_NUMBER_PATTERN = @"^[0-9]{5,9}$";
        }

    }
}
