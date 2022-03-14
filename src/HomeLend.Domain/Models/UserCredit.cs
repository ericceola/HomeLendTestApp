namespace HomeLend.Domain.Models
{
    public  class UserCredit
    {
        public bool HasCreditLimit { get; private set; }

        public decimal CreditLimit { get; private set; }

        public UserCredit(bool hasCreditLimit, decimal creditLimit = 0)
        {
            HasCreditLimit = hasCreditLimit;
            CreditLimit = creditLimit;
        }
    }
}
