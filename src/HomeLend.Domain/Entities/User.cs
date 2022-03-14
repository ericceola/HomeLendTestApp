using System;

namespace HomeLend.Domain.Entities
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool HasCreditLimit { get; set; }

        public decimal CreditLimit { get; set; }

        public int ClientId { get; set; }

        public User(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId, bool hasCreditLimi, decimal creditLimit)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new Exception("FistName is empty or null.");

            if (string.IsNullOrEmpty(lastName))
                throw new Exception("LastName is empty or null.");

            if (email.Contains("@") && !email.Contains("."))
                throw new Exception("E-mail is invalid.");

            DateTime now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                throw new Exception("Age less than 21 years old.");
            }

            if (hasCreditLimi && creditLimit < 500)
            {
                throw new Exception("User not added. No credit limit.");
            }

            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            DateOfBirth = dateOfBirth;
            ClientId = clientId;
            HasCreditLimit = hasCreditLimi;
            CreditLimit = creditLimit;

        }


    }
}
