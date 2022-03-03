using System;
namespace uniqlo_project.Models
{
    public class UpdatePersonalDataResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public DataInfo Data { get; set; }

        public class DataInfo
        {
            public UpdatesStatus Updates_status { get; set; }
            public PersonalData Personal_data { get; set; }
        }

        public class PersonalData
        {
            public string user_login { get; set; }
            public string user_email { get; set; }
            public string First_name { get; set; }
            public string Gender { get; set; }
            public string Birth_date { get; set; }
            public string Phone { get; set; }
            public string Avatar_url { get; set; }
        }

        public class UpdatesStatus
        {
            public DetailData login { get; set; }
            public DetailData email { get; set; }
            public DetailData First_name { get; set; }
            public DetailData Gender { get; set; }
            public DetailData Birth_date { get; set; }
            public DetailData Phone { get; set; }
            public DetailData Avatar_url { get; set; }
        }

        public class DetailData
        {
            public string Status { get; set; }
            public string Message { get; set; }
        }
    }
}
