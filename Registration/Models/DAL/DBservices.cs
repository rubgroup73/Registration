using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Registration.Models.DAL
{
/***********************************Insert User*******************/
    public class DBservices
    {
        public int insert(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("ConnectionStringPerson"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(user);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        /***********************************End Insert User********************************************/
        /***********************************User Confirmation In React App********************************/
        public User GetUserForConfirmation(string username, string constring, string tableName)
        {
            username = username.Trim();
            
            User user = new User();
            SqlConnection con = null;
            try
            {

                con = connect(constring); // create a connection to the database using the connection String defined in the web config file
                string getQuery = "SELECT * FROM " + tableName + " WHERE UserName='"+ username+"'";

                SqlCommand cmd = new SqlCommand(getQuery, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    user.UserName = (string)(dr["UserName"]);
                    user.Password = (string)(dr["User_password"]);
     
                }

                return user;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        /*************************Build the Insert command User String**********************************/
        private String BuildInsertCommand(User user)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}',{9},{10},{11},{12},'{13}','{14}',{15})", user.FullName, user.Gender.ToString(), user.BirthDate, user.Status, user.YearsOfEducation, user.UserName, user.Password, user.Mail, user.Phone, user.Residence, user.City, user.PrefDay1, user.PrefDay2, user.PrefHour1, user.PrefHour2, user.Score);
            String prefix = "INSERT INTO AppUser " + "( FullName, Gender,Birthday,Family_Status ,Education, UserName ,User_Password ,Mail,phone,Residence,City,prefday1,prefday2,prefhour1,prefhour2,score)";
            command = prefix + sb.ToString();

            return command;
        }
        /*********************END Build the Insert command User String*************************************/
        /*******************************End User Confirmation In React App********************************/

        /**************************************************************************************************/
        /*******************************Insert Class*******************************************************/
        /*************************************************************************************************/
        public int InsertClassToDB(AppClass appClass)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("ConnectionStringPerson"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommandClass(appClass);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        private String BuildInsertCommandClass(AppClass appClass)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values('{0}','{1}')", appClass.Description,appClass.Title);
            String prefix = "INSERT INTO Class " + "( class_desc,class_title)";
            command = prefix + sb.ToString();

            return command;
        }

        /**************************************************************************************************/
        /*******************************END Insert Class*******************************************************/
        /*************************************************************************************************/

        /**************************************************************************************************/
        /*******************************Get All Classes From DB********************************************/
        /*************************************************************************************************/

        public List<AppClass> GetAllClassFromDB(string tableName, string connectionString)
        {
             
            List<AppClass> allClass = new List<AppClass>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getClasses = "SELECT * FROM " + tableName;

                SqlCommand cmd = new SqlCommand(getClasses, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    AppClass appClass = new AppClass();
                    appClass.Id = Convert.ToInt32(dr["class_id"]);
                    appClass.Description = (string)(dr["class_desc"]);
                    appClass.Title = (string)dr["class_title"];
                    appClass.Status = Convert.ToInt32(dr["class_status"]);
                    appClass.Position = Convert.ToInt32(dr["approved_class_position"]);
                    appClass.Score = Convert.ToInt32(dr["score"]);
                    appClass.Version = Convert.ToInt32(dr["score"]);
                    //appClass.CreationDate = Convert.ToDateTime(dr["class_timestamp"]);
                    allClass.Add(appClass);
                }

                return allClass;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        /**************************************************************************************************/
        /******************************* END Get All Classes From DB***************************************/
        /*************************************************************************************************/

        /*************************************Create Sql Command*******************************/
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }
        /*************************************END Create Sql Command*******************************/
        /******************************Create Connection*************************************/
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        /******************************END Create Connection*************************************/
    }
}