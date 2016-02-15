using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Windows;

namespace aspContact
{

    class ContactSQL
    {

        /// <summary>
        /// Adds an adress to the database. Needs to be supplied with a customer ID.
        /// </summary>
        public int AddAdressToDatabase(Adresses adress, int thisCustomerID, SqlConnection connection)
        {
            SqlConnection myConnection = connection;

            int tID = 0;

            try
            {

                myConnection.Open();

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spAddAdress";


                SqlParameter paramCity = new SqlParameter("@City", SqlDbType.VarChar);
                SqlParameter paramPostal = new SqlParameter("@Postal", SqlDbType.VarChar);
                SqlParameter paramID = new SqlParameter("@NewIDA", SqlDbType.Int);
                SqlParameter paramStreet = new SqlParameter("@Street", SqlDbType.VarChar);

                paramID.Direction = ParameterDirection.Output;

                paramStreet.Value = adress.Street;
                paramCity.Value = adress.City;
                paramPostal.Value = adress.Postal;

                myCommand.Parameters.Add(paramStreet);
                myCommand.Parameters.Add(paramCity);
                myCommand.Parameters.Add(paramPostal);
                myCommand.Parameters.Add(paramID);


                myCommand.ExecuteNonQuery();

                tID = (int)paramID.Value;

                myCommand = new SqlCommand();
                myCommand.Connection = connection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spConnectAIDtoCID";

                SqlParameter paramCID = new SqlParameter("@CID", SqlDbType.Int);
                SqlParameter paramAID = new SqlParameter("@AID", SqlDbType.Int);

                paramCID.Value = thisCustomerID;
                paramAID.Value = tID;

                myCommand.Parameters.Add(paramCID);
                myCommand.Parameters.Add(paramAID);
                myCommand.ExecuteNonQuery();

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { myConnection.Close(); }


            return tID;
        }

        /// <summary>
        /// Cleares the whole contact database
        /// </summary>
        /// <param name="connection"></param>
        public void ClearDatabase(SqlConnection connection)
        {
            SqlConnection thisConnection = connection;


            try
            {
                thisConnection.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = thisConnection;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spDeleteAllContact";

                myCommand.ExecuteNonQuery();


            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { thisConnection.Close(); }

        }

        /// <summary>
        /// Deletes record for given contact ID
        /// </summary>
        public void DeleteContactFromDatabase(int thisID, SqlConnection connection)
        {
            SqlConnection thisConnection = connection;

            try
            {
                thisConnection.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = thisConnection;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spDeleteContact";

                SqlParameter IDParam = new SqlParameter("@ID", SqlDbType.Int);
                IDParam.Value = thisID;
                myCommand.Parameters.Add(IDParam);

                myCommand.ExecuteNonQuery();
                
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { thisConnection.Close(); }

        }

        /// <summary>
        /// Returns a customer ID based on customer social security input
        /// </summary>
        public int GetCustomerID(String thisSSN, SqlConnection connection)
        {
            SqlConnection thisConnection = connection;

            int thisID = 0;

            try
            {
                thisConnection.Open();

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = thisConnection;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spGetIDfromSSN";

                SqlParameter paramSSN = new SqlParameter("@SSN", SqlDbType.VarChar);

                paramSSN.Value = thisSSN;
                myCommand.Parameters.Add(paramSSN);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                    thisID = (int)myReader["ID"];


            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { thisConnection.Close(); }

            return thisID;

        }

        public int GetContactIDfromName(String thisFirstName, SqlConnection connection)
        {

            SqlConnection thisConnection = connection;

            int thisID = -1;
            
            thisConnection.Open();

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = thisConnection;

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "spGetContactIDFromNameLike";

            SqlParameter paramFirstName = new SqlParameter("@firstName", SqlDbType.VarChar);

            paramFirstName.Value = thisFirstName;
            myCommand.Parameters.Add(paramFirstName);

            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
                thisID = (int)myReader["ID"];

            return thisID;


        }

        /// <summary>
        /// Gets all the contacts from the database
        /// </summary>
        public List<Person> OpenDatabase(SqlConnection connection)
        {
            SqlConnection thisConnection = connection;
            List<Person> thesePersons = new List<Person>();

            try
            {
                thisConnection.Open();

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = thisConnection;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spGetAllContacts";

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                    thesePersons.Add(new Person(myReader["Firstname"].ToString(), myReader["Lastname"].ToString(), myReader["SSN"].ToString(), myReader["emailAdress"].ToString()));

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { thisConnection.Close(); }

            return thesePersons;
            
        }

        /// <summary>
        /// Checks if a person exists in the db
        /// </summary>
        public bool PersonExits(Person person, SqlConnection connection)
        {

            SqlConnection thisConnection = connection;
            thisConnection.Open();

            string sqlQuery = "SELECT Count(*) FROM CONTACT ";
            sqlQuery += "WHERE SSN = '" + person.SocialSecurity + "'";

            SqlCommand myCommand = new SqlCommand(sqlQuery, thisConnection);
            int usersWithThisSSN = (int)myCommand.ExecuteScalar();
            thisConnection.Close();
            

            if (usersWithThisSSN > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Adds a person to the database and returns the customer ID
        /// </summary>
        public int AddPersonToDatabase(Person person, SqlConnection connection)
        {
            SqlConnection myConnection = connection;

            int tID = 0;

            try
            {

                myConnection.Open();

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spAddContact";

                SqlParameter paramFirstName = new SqlParameter("@firstName", SqlDbType.VarChar);
                SqlParameter paramLastName = new SqlParameter("@lastName", SqlDbType.VarChar);
                SqlParameter paramSSN = new SqlParameter("@SSN", SqlDbType.VarChar);
                SqlParameter paramEmailAdress = new SqlParameter("@emailAdress", SqlDbType.VarChar);
                SqlParameter paramID = new SqlParameter("@newID", SqlDbType.Int);

                paramID.Direction = ParameterDirection.Output;

                paramFirstName.Value = person.FirstName;
                paramLastName.Value = person.LastName;
                paramSSN.Value = person.SocialSecurity;
                paramEmailAdress.Value = person.EmailAdress;

                myCommand.Parameters.Add(paramFirstName);
                myCommand.Parameters.Add(paramLastName);
                myCommand.Parameters.Add(paramSSN);
                myCommand.Parameters.Add(paramID);
                myCommand.Parameters.Add(paramEmailAdress);

                myCommand.ExecuteNonQuery();

                tID = (int)paramID.Value;


            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { myConnection.Close(); }


            return tID;
        }

        public void ChangePersonInDatabase(Person person, int IDToChange, SqlConnection connection)
        {
            SqlConnection myConnection = connection;

            int thisID = IDToChange;

            try
            {

                myConnection.Open();

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spChangeContact";

                SqlParameter paramFirstName = new SqlParameter("@firstName", SqlDbType.VarChar);
                SqlParameter paramLastName = new SqlParameter("@lastName", SqlDbType.VarChar);
                SqlParameter paramID = new SqlParameter("@ID", SqlDbType.Int);

                paramFirstName.Value = person.FirstName;
                paramLastName.Value = person.LastName;
                paramID.Value = thisID;

                myCommand.Parameters.Add(paramFirstName);
                myCommand.Parameters.Add(paramLastName);
                myCommand.Parameters.Add(paramID);

                myCommand.ExecuteNonQuery();


            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { myConnection.Close(); }

        }

        public List<Adresses> GetAdressesFromID(int thisID, SqlConnection connection)
        {
            List<Adresses> theseAdresses = new List<Adresses>();
            SqlConnection thisConnection = connection;
            try
            {

                thisConnection.Open();

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = thisConnection;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "spGetAdressesFromID";

                SqlParameter paramID = new SqlParameter("@CID", SqlDbType.VarChar);

                paramID.Value = thisID;
                myCommand.Parameters.Add(paramID);

                SqlDataReader myReader = myCommand.ExecuteReader();
                
                while (myReader.Read())
                    theseAdresses.Add(new Adresses(myReader["Street"].ToString(), myReader["City"].ToString(), myReader["Postal"].ToString()));

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { thisConnection.Close(); }

            return theseAdresses;
        }

    }
}
