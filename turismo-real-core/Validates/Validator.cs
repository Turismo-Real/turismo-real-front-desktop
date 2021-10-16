using System;
using System.Net.Mail;

namespace turismo_real_controller.Validates
{
    public class Validator
    {
        public bool EmailValidator(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateEmptyString(string content)
        {
            if (content.CompareTo(String.Empty) != 0) return true;
            return false;
        }
    }
}
