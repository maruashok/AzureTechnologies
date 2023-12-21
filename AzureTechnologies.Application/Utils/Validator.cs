using System.Text.RegularExpressions;

namespace AzureTechnologies.Application.Utils
{
    public class Validator
    {
        public static bool IsEmailValid(string? email)
        {
            return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        public static bool IsStrongPassword(string? password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 6 && Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
        }
    }
}