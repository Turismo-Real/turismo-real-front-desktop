﻿namespace turismo_real_business.DTOs
{
    public class LoginDTO
    {
        public LoginDTO() { }

        public LoginDTO(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public string email { get; set; }
        public string password { get; set; }
    }
}
