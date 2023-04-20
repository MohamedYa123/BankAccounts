namespace bankAccounts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //defining retail customer
            RetailsCustomer retailsCustomer= new RetailsCustomer();
            retailsCustomer.FirstName = "Mohamed";
            retailsCustomer.LastName = "Yasser";
            retailsCustomer.PhoneNumber= "+1234567890";
            //defining accounts
            LoanAccount loanAccount= new LoanAccount();
            SavingsAccount savingsAccount= new SavingsAccount();
            BasicAccount basicAccount= new BasicAccount();

            retailsCustomer.Accounts.Add(loanAccount);
            retailsCustomer.Accounts.Add(savingsAccount);
            retailsCustomer.Accounts.Add(basicAccount);
            //deposit
            for(int i = 0; i < retailsCustomer.Accounts.Count; i++)
            {
                retailsCustomer.Accounts[i].Deposit(100);
            }
            //withdraw
            for (int i = 0; i < retailsCustomer.Accounts.Count; i++)
            {
                retailsCustomer.Accounts[i].Withdraw(60);
            }
            Console.WriteLine(retailsCustomer.ToString());

            //defining indvidualCustomer
            IndividualCustomer individualCustomer= new IndividualCustomer();
            individualCustomer.FirstName = "Mohamed";
            individualCustomer.LastName = "Yasser";
            SalaryAccount salaryAccount2 = new SalaryAccount();
            LoanAccount loanAccount2 = new LoanAccount();
            SavingsAccount savingsAccount2 = new SavingsAccount();
            BasicAccount basicAccount2 = new BasicAccount();
            individualCustomer.Accounts.Add(salaryAccount2);
            individualCustomer.Accounts.Add(loanAccount2);
            individualCustomer.Accounts.Add(savingsAccount2);
            individualCustomer.Accounts.Add(basicAccount2);
            //deposit
            for (int i = 0; i < individualCustomer.Accounts.Count; i++)
            {
                try
                {
                    individualCustomer.Accounts[i].Deposit(100);
                }
                catch { }
            }
            //withdraw
            for (int i = 0; i < individualCustomer.Accounts.Count; i++)
            {
                try
                {
                    individualCustomer.Accounts[i].Withdraw(80);
                }
                catch { }
            }
            Console.WriteLine(individualCustomer.ToString());

        }
    }
}