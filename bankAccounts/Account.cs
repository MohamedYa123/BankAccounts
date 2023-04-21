using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankAccounts
{
    public abstract class Account
    {

        #region fields
        protected string type = "";
        protected double balance;
        DateTime createddate;
        #endregion
        #region Properties
        public string Type { get { return type; } }
        public double Balance { get { return balance; } }
        public bool IsClosed { get; set; }
        public DateTime CreatedDate { get { return createddate; } set { checkClosed();createddate = value; } }
        #endregion
        #region methods
        /// <summary>
        /// throws exception if the account is closed to disallow the set operation
        /// </summary>
        /// <exception cref="Exception"></exception>
        protected void checkClosed()
        {
            if (IsClosed)
            {
                throw new Exception("Cannot to do operations on closed accounts");
            }
        }
        /// <summary>
        /// Deposit certain amount to account Balance
        /// </summary>
        /// <param name="amount"></param>
        /// <exception cref="Exception"></exception>
        public virtual void Deposit(double amount)
        {

            if(type== "SalaryAccount")
                throw new Exception("no deposit allowedon salary accounts");
            checkClosed();
            balance += amount;
        }
        /// <summary>
        /// Withdraw a certain amount from account Balance
        /// </summary>
        /// <param name="amount"></param>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Call to delete the current account
        /// to delete account its Balance has to be zero
        /// No operation is allowed on a deleted account
        /// </summary>
        /// <exception cref="Exception"></exception>
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
        #endregion
    }
    public class SalaryAccount : Account
    {
        /// <summary>
        /// Create instance of SalaryAccount class
        /// Cannot be deposited from deposit function
        /// </summary>
        public SalaryAccount() {
            type = "SalaryAccount";
        }
    }
    public class LoanAccount : Account
    {
        /// <summary>
        /// Create instance of LoanAccount class
        /// Can be deposited with certain amount of Balance
        /// </summary>
        public LoanAccount() { type = "LoanAccount"; }
    }
    public class SavingsAccount : Account
    {
        /// <summary>
        /// Create instance of SavingsAccount class
        /// Can be deposited with certain amount of Balance
        /// </summary>
        public SavingsAccount() { type = "SavingsAccount"; }
    }
    public class BasicAccount : Account
    {
        /// <summary>
        /// Create instance of BasicAccount class
        /// Can be deposited with certain amount of Balance
        /// </summary>
        public BasicAccount() { type = "BasicAccount"; }
    }
}
