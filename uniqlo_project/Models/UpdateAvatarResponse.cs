using System;
namespace uniqlo_project.Models
{
    public class UpdateAvatarResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public DataInfo Data { get; set; }

        public class DataInfo
        {
            public string Avatar_url { get; set; }
        }
    }
}
