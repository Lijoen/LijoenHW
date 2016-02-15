using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using aspContact;

namespace TestMaster
{

    public partial class WebForm4 : System.Web.UI.Page
    {
        const string CON_STRING = "Data Source=ACADEMY007-VM; Initial Catalog=Contacts; Integrated Security=SSPI";
        SqlConnection contactConnection = new SqlConnection(CON_STRING);
        ContactSQL sql = new ContactSQL();
        Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            int pWidth = random.Next(1, 2000);
            int hWidth = random.Next(1, 2000);
            int zDim = pWidth * hWidth * pWidth;

            try
            {
                zDim = zDim % (hWidth * pWidth * pWidth);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            finally
            {
                int dbz = (pWidth / zDim);
            }
        }


        protected int getGrid(int pWidth, int hWidth)
        {
            return (pWidth * hWidth * pWidth);
        }

        protected void bAddAdress_Click(object sender, EventArgs e)
        {
            int thisID = -1;
            string thisSSN = contactGrid.SelectedRow.Cells[4].Text;
            Console.WriteLine("SNN is: " + thisSSN);
            thisID = sql.GetCustomerID(thisSSN, contactConnection);
                
            if (thisID == -1)
                throw new Exception("ID is empty");

            Adresses thisAdress = new Adresses(txStreet.Text, txCity.Text, "");
            sql.AddAdressToDatabase(thisAdress, thisID, contactConnection);

            adressGrid.DataBind();


            
        }

        protected void contactGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (contactGrid.SelectedIndex < 0)
            {
                bAddAdress.Visible = false;
                txCity.Visible = false;
                lCity.Visible = false;
                txStreet.Visible = false;
                lStreet.Visible = false;
                lAddAdress.Visible = false;

                lAddAdress.Text = "";

                return;
            }

            bAddAdress.Visible = true;
            txCity.Visible = true;
            lCity.Visible = true;
            txStreet.Visible = true;
            lStreet.Visible = true;
            lAddAdress.Visible = true;
            lAddAdress.Text = "Add Adress To " + contactGrid.SelectedRow.Cells[2].Text;

        }

        protected void contactGrid_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if(contactGrid.SelectedIndex < 0)
            {
                bAddAdress.Visible = false;
                txCity.Visible = false;
                lCity.Visible = false;
                txStreet.Visible = false;
                lStreet.Visible = false;
                lAddAdress.Visible = false;
                lAddAdress.Text = "";
                lAdresses.Visible = false;

                return;

            }

            bAddAdress.Visible = true;
            txCity.Visible = true;
            lCity.Visible = true;
            txStreet.Visible = true;
            lStreet.Visible = true;
            lAddAdress.Visible = true;
            lAddAdress.Text = "Add Adress To " + contactGrid.SelectedRow.Cells[2].Text;
            lAdresses.Visible = true;

        }

        protected void SetVisibility()
        {

            if (contactGrid.SelectedIndex < 0)
            {
                bAddAdress.Visible = false;
                txCity.Visible = false;
                lCity.Visible = false;
                txStreet.Visible = false;
                lStreet.Visible = false;
                lAddAdress.Visible = false;
                lAddAdress.Text = "";
                lAdresses.Visible = false;

                return;

            }


            bAddAdress.Visible = true;
            txCity.Visible = true;
            lCity.Visible = true;
            txStreet.Visible = true;
            lStreet.Visible = true;
            lAddAdress.Visible = true;
            lAddAdress.Text = "Add Adress To " + contactGrid.SelectedRow.Cells[2].Text;
            lAdresses.Visible = true;
            
        }

        protected void txSearchFirstName_TextChanged(object sender, EventArgs e)
        {

            contactGrid.DataSourceID = SqlDataSourceFIlterName.ID;
            contactGrid.DataBind();
        }

        protected void bSearch_Click(object sender, EventArgs e)
        {

            contactGrid.SelectedIndex = -1;
            
            contactGrid.DataSourceID = SqlDataSourceFIlterName.ID;
            contactGrid.DataBind();
            

            if (txSearchFirstName.Text == "")
            {
                contactGrid.DataSourceID = SqlDataSource3.ID;
                contactGrid.DataBind();

            }

            SetVisibility();

        }


        /// <summary>
        /// recursive get grid index per page; returns result row or iterative call for next page or zero if no more pages
        /// </summary>
        protected int getGridIndex(string firstName, int page)
        {

            contactGrid.PageIndex = page;
            
            int searchedRow = 0;

            for(int i = 0; i<10; i++)
            {
                if (contactGrid.Rows[i].Cells[2].Text.ToUpper() == firstName.ToUpper())
                {
                    return searchedRow;
                }
                searchedRow++;
            }

            if (page == (contactGrid.PageCount - 1))
                return -1;    
            else 
                return (getGridIndex(firstName, page + 1));
            
        }

        protected void adressGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    
}