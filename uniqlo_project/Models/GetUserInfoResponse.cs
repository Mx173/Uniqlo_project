using System;
namespace uniqlo_project.Models
{
    public class GetUserInfoResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public DataInfo Data { get; set; }

        public class DataInfo
        {
            public string User_login { get; set; }
            public string User_email { get; set; }
            public string First_name { get; set; }
            public string Gender { get; set; }
            public string Birth_date { get; set; }
            public string Phone { get; set; }
            public string Avatar_url { get; set; }
        }
    }

    
}
