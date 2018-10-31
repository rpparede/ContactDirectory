using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact.econtactClasses
{
    class contactClass
    {
        //Getter and setter properies 
        //acts as data carrier in app 
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Selecting Data from Database
        public DataTable Select()
        {
            //Step1 Database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Step 2 write sql query
                string sql = "SELECT * FROM tbl_contact";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {


            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Inserting data into data base 
        public bool Insert(contactClass c)
        {
            //Create a default return type and set def value to false 
            bool isSuccess = false;

            //step 1 connect db 
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //step 2 create SQL Query to insert data 

                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo,Address,Gender) VALUES(@FirstName,@LastName,@ContactNo,@Address,@Gender)";

                //cREATE SQL COMMAND USING SQL AND CONN 
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add data 
                cmd.Parameters.AddWithValue("@FirstName",c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //connection 
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if query runs successfully then the calue of rows will be greather than 0 else less 
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                    isSuccess = false;

            }   
            catch(Exception ex)
            {


            }
            finally
            {
                conn.Close();

            }
            return isSuccess;
        }

        //Method to update database from our app 

        public bool Update(contactClass c)
        {
            //Create a default return and set value to false 
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL to update data in our dat base 
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                //create sql command 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create param to add value 
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                    isSuccess = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        public bool Delete (contactClass c)
        {
            //Create a default return value and set its calue to ofalse 
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_contact WHERE ContactID = @ContactID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if query runs successfully then the calue of rows will be greather than 0 else less 
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                    isSuccess = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
