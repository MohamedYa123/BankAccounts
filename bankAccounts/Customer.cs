using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bankAccounts
{
    public abstract class Customer
    {

        #region fields
        static string onlyletters = "([a-zA-Z][a-zA-Z]+|[ا-ي][ا-ي]+)";
        static Regex regletters=new Regex(onlyletters);
        string firstname;
        string lastname;
        DateOnly birthdate;
        DateTime createddate;
        string address;
        static string phonenumbereg = @"^\+?\(?([0-9]{3})\)?[-.● ]?([0-9]{3})[-.● ]?([0-9]{4})$";
        static Regex phoneregex = new Regex(phonenumbereg);
        string phonenumber;
        #endregion
        #region properties
        /// <summary>
        /// FirstName property
        /// Rules :
        /// 1-only contains alphabetic letters
        /// 2-at least two letters are allowed
        /// </summary>
        public string FirstName { get {
                return firstname;
            } set { 
                checkDelete();
                if(regletters.Match(value).Length==value.Length)
                {
                    firstname = value;
                }
                else
                {
                    throw new Exception("Name has to be more than 2 letters and only engilsh letters");
                }
                
            } }
        /// <summary>
        /// LastName property
        /// Rules :
        /// 1-only contains alphabetic letters
        /// 2-at least two letters are allowed
        /// </summary>
        public string LastName { get { return lastname; } set { 
            
                checkDelete();
                if (regletters.Match(value).Length == value.Length)
                {
                    lastname = value;
                }
                else
                {
                    throw new Exception("Name has to be more than 2 letters and only engilsh letters");
                }
            } }
        
        public DateOnly BirthDate { get { return birthdate; } set {
                checkDelete();
                birthdate= value;
            } }
        
        protected string Address { get { return address; } set { checkDelete();
                address= value;
            } }
        
        public DateTime CreatedDate { get { return createddate; } set { 
                checkDelete();
                createddate= value;
            } }
        public bool IsDeleted { get; set; }=false;
        public listofaccounts Accounts { get; set; }
        /// <summary>
        /// Phone number property
        /// allows you to set the phone number of the customer
        /// Rules :
        /// 1- no letters allowed 
        /// + sign only allowed at the begining of the phone number
        /// only known PhoneNumber formats are allowed
        /// </summary>
        public string PhoneNumber { get {
                return phonenumber;
            } set {
                checkDelete();
                if(phoneregex.Match(value).Length==value.Length)
                {
                    phonenumber = value;
                }
                else
                {
                    throw new Exception("Invalid PhoneNumber");
                }
            } }
        #endregion
        #region methods
        /// <summary>
        /// throws exception if the customer is deleted to disallow the ongoing operation
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void checkDelete()
        {
            if (IsDeleted)
            {
                throw new Exception("Cannot do operations on deleted Customers !");
            }
        }
        /// <summary>
        /// Deletes the Customer
        /// No operation is allowed on a deleted customer
        /// </summary>
        public void Delete()
        {
            IsDeleted=true;
        }
        /// <summary>
        /// returns a string field representing the current customer 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            double totalbalance = 0;
            for(int i=0;i<Accounts.Count;i++)
            {
                totalbalance += Accounts[i].Balance;
            }
            return $"Customer : {FirstName} {LastName} \r\nTotal Balance : {totalbalance}";
        }
        #endregion
    }
    public class IndividualCustomer : Customer
    {
        /// <summary>
        /// Create an instance of IndividualCustomer
        /// </summary>
        public IndividualCustomer()
        {
            Accounts = new listofaccounts(this, false);
        }
        #region properties
        public string HomeAddress
        {
            
            get
            {
                return base.Address;
            }
            set
            {
                //no need to call checkdelete function as it is called on customer class already
                base.Address = value;
            }
        }
        #endregion
    }
    public class RetailsCustomer : Customer
    {
        #region fields
        static string emailreg= @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        static Regex regexemail= new Regex(emailreg);
        string email;
        #endregion
        /// <summary>
        /// Create instance of RetailsCustomer
        /// </summary>
        public RetailsCustomer()
        {
           Accounts = new listofaccounts(this,true);
        }
        #region properties
        public string CompanyAddress
        {
            get
            {
                return base.Address;
            }
            set
            {
                //no need to call checkdelete function as it is called on customer class already
                base.Address = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                checkDelete();
                if(regexemail.Match(value).Length==value.Length)
                {
                    email=value;
                }
                else
                {
                    throw new Exception("Invalid email");
                }
                
            }
        }
        #endregion
    }
    public class listofaccounts
    {
        /// <summary>
        /// Create instance of listofaccounts class
        /// it is used to aply rules of adding accounts to the lis
        /// </summary>
        public listofaccounts() { }
        #region fields
        Customer owner;
        bool nosalary;
        List<Account> Accounts = new List<Account>();
        #endregion
        #region properties
        public int Count { get { return Accounts.Count; } }
        #endregion
        #region methods
        public listofaccounts(Customer owner,bool nosalary)
        {
            this.owner=owner;
            this.nosalary = nosalary;
        }
        public void Add(Account account) {
            owner.checkDelete();
            if ( account.Type!= "SalaryAccount" || !nosalary)
            {
                Accounts.Add(account);
            }
            else
            {
                throw new Exception("Retalis account canot have a salary account");
            }
        }
        public void Remove(Account acc)
        {
            owner.checkDelete();
            Accounts.Remove(acc);
        }
        public void RemoveAt(int id)
        {
            owner.checkDelete();
            Accounts.RemoveAt(id);
            Accounts.GetEnumerator();
        }
        public Account this[int id]
        {
            get
            {
                return Accounts[id];
            }
            set
            {
                owner.checkDelete();
                if (value.Type != "SalaryAccount" || !nosalary)
                {
                    Accounts[id] = value;
                }
                else
                {
                    throw new Exception("Retalis account cannot have a salary account");
                }
            }
        }
        #endregion
    }
}
