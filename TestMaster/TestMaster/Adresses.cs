using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspContact
{

    public class Adresses
    {
        private string street;
        private string city;
        private string postal;

        public Adresses(string s, string c = "", string p = "")
        {
            this.street = s;
            this.city = c;
            this.postal = p;
        }

        #region properties
        public string Postal
        {
            get { return postal; }
            set { postal = value; }
        }



        public string City
        {
            get { return city; }
            set { city = value; }
        }



        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        #endregion
    }
}
