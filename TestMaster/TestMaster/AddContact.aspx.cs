using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using aspContact;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.VisualBasic;
using testLibrary;

namespace TestMaster
{
    public partial class WebForm3 : System.Web.UI.Page
    { 
        const string CON_STRING = "Data Source=ACADEMY007-VM; Initial Catalog=Contacts; Integrated Security=SSPI";
        SqlConnection contactConnection = new SqlConnection(CON_STRING);
        ContactSQL sql = new ContactSQL();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            lSSNExists.Text = "";
            lSSNExists.Visible = false;

            while(true)
            {

            }

        }

        protected void bAddUser_Click(object sender, EventArgs e)
        {
            List<Person> contacts = new List<Person>();

            

            if ((txFirstName.Text == "") || (txLastName.Text == ""))
            {
                lSSNExists.Visible = true;
                lSSNExists.Text = "Please provide first and last names";
                return;
            }

            Person thisPerson = new Person();

            try
            {
                thisPerson = new Person(txFirstName.Text, txLastName.Text, txSocialSecurity.Text, txEmailAdress.Text);
            }
            catch (Exception ex)
            {
                lSSNExists.Visible = true;
                lSSNExists.Text = ex.Message;
                return;
            }

            if (sql.PersonExits(thisPerson, contactConnection))
            {
                
                lSSNExists.Visible = true;
                lSSNExists.Text = "Social security " + txSocialSecurity.Text + " already exist ";

                return;
            }

            contacts.Add(thisPerson);

            int thisID = sql.AddPersonToDatabase(contacts.Last(), contactConnection);

            txFirstName.Text = "";
            txLastName.Text = "";
            txSocialSecurity.Text = "";
            txEmailAdress.Text = "";

            bAddUser.Enabled = false;
        }

        protected void txSocialSecurity_TextChanged(object sender, EventArgs e)
        {
            lSSNExists.Text = "";
            lSSNExists.Visible = false;
        }
    }
}