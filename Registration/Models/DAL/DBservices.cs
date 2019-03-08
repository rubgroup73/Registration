//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Web;
//using System.Web.Configuration;

//namespace Registration.Models.DAL
//{
//    public class DBservices
//    {
//        public int insert(Person person)
//        {

//            SqlConnection con;
//            SqlCommand cmd;
           

//            try
//            {
//                con = connect("ConnectionStringPerson"); // create the connection
//            }
//            catch (Exception ex)
//            {
//                // write to log
//                throw (ex);
//            }

//            String cStr = BuildInsertCommand(person);      // helper method to build the insert string
//            cmd = CreateCommand(cStr, con);             // create the command

//            try
//            {
//                cmd = CreateCommand(cStr, con);
//                int numAffected = cmd.ExecuteNonQuery();
//                return numAffected;
//            }
//            catch (SqlException ex)
//            {
//                // the exception alone won't tell you why it failed...
//                if (ex.Number == 2627)
//                {
//                    throw new Exception("The user is already exists");
//                }
//                return 0;
//            }
//            catch (Exception ex)
//            {
//                return 0;
//                throw (ex);
//            }
//            finally
//            {
//                if (con != null)
//                {
//                    // close the db connection
//                    con.Close();
//                }
//            }
//        }

//        /*************************END Build the Insert command String**********************************/
//        private String BuildInsertCommand(Person person)
//                {
//                    String command;

//                    StringBuilder sb = new StringBuilder();
//                    // use a string builder to create the dynamic string
//                    sb.AppendFormat("Values('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},{8},'{9}','{10}','{11}')", person.Name, person.FamilyName, person.Age.ToString(), person.Height.ToString(), person.Gender, person.Address, person.Img, person.Active.ToString(), person.Premium.ToString(), person.Tel, person.UserName, person.Password);
//                    String prefix = "INSERT INTO personTblNew " + "( name, familyName,age,height,gender, address,img,active,premium,phone,username,password) output INSERTED.id ";
//                    command = prefix + sb.ToString();

//                    return command;
//                }
// /*********************END Build the Insert command String*************************************/

// /*************************************Create Sql Command*******************************/
//        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
//        {

//            SqlCommand cmd = new SqlCommand(); // create the command object

//            cmd.Connection = con;              // assign the connection to the command object

//            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

//            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

//            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

//            return cmd;
//        }
///*************************************END Create Sql Command*******************************/
///******************************Create Connection*************************************/
//        public SqlConnection connect(String conString)
//        {

//            // read the connection string from the configuration file
//            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
//            SqlConnection con = new SqlConnection(cStr);
//            con.Open();
//            return con;
//        }
///******************************END Create Connection*************************************/
//    }
//}