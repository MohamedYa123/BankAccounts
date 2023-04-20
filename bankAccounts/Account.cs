using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankAccounts
{
    public abstract class Account
    {
        protected void checkClosed()
        {
            if(IsClosed)
            {
                throw new Exception("Cannot to do operations on closed accounts");
            }
        }
        protected string type = "";
        public string Type { get { return type; } }
        protected double balance;
        public double Balance { get { return balance; } }
        public bool IsClosed { get; set; }
        DateTime createddate;
        public DateTime CreatedDate { get { return createddate; } set { checkClosed();createddate = value; } }
        public virtual void Deposit(double amount)
        {
            throw new Exception("no deposit allowed");
        }
        public void Withdraw(double amount)
        {
            checkClosed();
            if(Balance>=amount)
            {
                balance -= amount;
            }
            else
            {
                throw new Exception("insufficient funds to withdraw");
            }
        }
        public void Delete()
        {
            if (Balance == 0)
            {
                IsClosed = true;
            }
            else
            {
                throw new Exception("Cannot close account with balance more than 0");
            }
        }
    }
    public class SalaryAccount : Account
    {
        public SalaryAccount() {
            type = "SalaryAccount";
        }
    }
    public class LoanAccount : Account
    {
        public override void Deposit(double amount)
        {
            checkClosed();
            balance += amount;
        }

    }
    public class SavingsAccount : Account
    {
        public override void Deposit(double amount)
        {
            checkClosed();
            balance += amount;
        }
    }
    public class BasicAccount : Account
    {
        public override void Deposit(double amount)
        {
            checkClosed();
            balance += amount;
        }
    }
}
