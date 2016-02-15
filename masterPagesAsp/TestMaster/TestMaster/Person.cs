using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspContact
{
    class Person
    {
        private String firstName;
        private String lastName;
        private String socialSecurity;
        private string emailAdress;

        public string EmailAdress
        {
            get { return emailAdress; }
            set { emailAdress = value; }
        }


        public Person()
        { }

        public Person(string fn, string ln, string ssn, string email)
        {

            if ((ssn.Count() < 10) || ((ssn.Count() > 10)))
                throw new Exception("SSN needs to be 10 characters");
            else
                this.socialSecurity = ssn;

            if((this.firstName == ""))
                throw new Exception("If you dont have a first name, get one");
            else
                this.firstName = fn;

            if (this.lastName == "")
                throw new Exception("If you dont have a last name, get one");
            else
                this.lastName = ln;

            this.emailAdress = email;
        }

        public String SocialSecurity
        {
            get { return this.socialSecurity; }
        }
        
        public String LastName
        {
            get { return lastName; }
        }


        public String FirstName
        {
            get { return firstName; }
        }



    }
}
