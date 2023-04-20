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
        public void checkDelete()
        {
            if (IsDeleted)
            {
                throw new Exception("Cannot do operations on deleted Customers !");
            }
        }
        static  string onlyletters = "[a-zA-Z][a-zA-Z]+";
        static Regex regletters=new Regex(onlyletters);
        string firstname;
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
        string lastname;
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
        DateOnly birthdate;
        public DateOnly BirthDate { get { return birthdate; } set {
                checkDelete();
                birthdate= value;
            } }
        string addredd;
        protected string Addredd { get { return addredd; } set { checkDelete();
                addredd= value;
            } }
        DateTime createddate;
        public DateTime CreatedDate { get { return createddate; } set { 
                checkDelete();
                createddate= value;
            } }
        public bool IsDeleted { get; set; }=false;
        
        static string phonenumbereg= @"^\+*\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$";
        static Regex phoneregex = new Regex(phonenumbereg);
        string phonenumber;
        public listofaccounts Accounts { get; set; }
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
        public void Delete()
        {
            IsDeleted=true;
        }
        public override string ToString()
        {
            double totalbalance = 0;
            for(int i=0;i<Accounts.Count;i++)
            {
                totalbalance += Accounts[i].Balance;
            }
            return $"Customer : {FirstName} {LastName} \r\nTotal Balance : {totalbalance}";
        }
    }
    public class IndividualCustomer : Customer
    {
        public IndividualCustomer()
        {
            Accounts = new listofaccounts(this, false);
        }
        public string HomeAddredd
        {
            
            get
            {
                return base.Addredd;
            }
            set
            {
                checkDelete();
                base.Addredd = value;
            }
        }
    }
    public class RetailsCustomer : Customer
    {
        static string emailreg= @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        static Regex regexemail= new Regex(emailreg);
        public RetailsCustomer()
        {
           Accounts = new listofaccounts(this,true);
        }
        public string CompanyAddredd
        {
            get
            {
                return base.Addredd;
            }
            set
            {
                checkDelete();
                base.Addredd = value;
            }
        }
        string email;
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
    }
    public class listofaccounts
    {
        Customer owner;
        bool nosalary;
        public listofaccounts(Customer owner,bool nosalary)
        {
            this.owner=owner;
            this.nosalary = nosalary;
        }
        List<Account> Accounts =new List<Account>();
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

        public int Count { get { return Accounts.Count; } }
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
    }
}
