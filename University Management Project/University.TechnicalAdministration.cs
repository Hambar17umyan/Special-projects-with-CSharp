using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_Project
{
    internal partial class University
    {
        private class TechnicalAdministration
        {
            public string EmailAdress;

            private static string Format(string s)
            {
                StringBuilder sb = new StringBuilder();
                foreach(char c in s)
                {
                    if (c == ' ')
                        continue;
                    sb.Append((c >= 'A' && c <= 'Z') ? (char)((c - 'A') + 'a') : c);
                }
                return sb.ToString();
            }
            public TechnicalAdministration(UniversityInputModel university)
            {
                EmailAdress = $"management{university.ID}@gmail.com";
            }

            public void ChangeEmail(string domain)
            {
                EmailAdress = "management@" + Format(domain) + ".com";
            }

            public Message SendEmailToStudent(string email, string text)
            {
                Console.WriteLine($"Email from {EmailAdress} to {email}:\n{text}\n{new string('-', 35)}");

                return Message.Success;
            }
        }
    }
}
