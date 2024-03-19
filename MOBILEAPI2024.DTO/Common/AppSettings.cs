﻿namespace MOBILEAPI2024.DTO.Common
{
    public class AppSettings
    {
        public string JWTTokenGenKey { get; set; }
        public string APIUri { get; set; }
        public EmailServiceOptions EmailServiceOptions { get; set; }
    }
}
