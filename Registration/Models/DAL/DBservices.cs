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
        /**************************************************************************************************/
        /***********************************User Confirmation In React App********************************/
        /************************************************************************************************/

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

        /************************************************************************************************/
        /*************************Build the Insert command User String**********************************/
        /************************************************************************************************/

        private String BuildInsertCommand(User user)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}',{9},{10},{11},{12},{13},{14},{15},{16})", user.FullName, user.Gender.ToString(), user.BirthDate, user.Status, user.YearsOfEducation, user.UserName, user.Password, user.Mail, user.Phone, user.Residence, user.City, user.PrefDay1, user.PrefHour1, user.PrefHour2, user.Score,user.Group_Id,user.Group_Version);
            String prefix = "INSERT INTO AppUser " + "( FullName, Gender,Birthday,Family_Status ,Education, UserName ,User_Password ,Mail,phone,Residence,City,prefday1,prefhour1,prefhour2,score,group_id,group_version)";
            command = prefix + sb.ToString();

            return command;
        }
        /**************************************************************************************************/
        /*******************************Insert Class*******************************************************/
        /*************************************************************************************************/
        public List<User> GetAllUsersFromDB(string tableName,string connectionString)
        {

            List<User> allUsers = new List<User>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                //string getUsers = "SELECT * FROM " + tableName + " where group_id = -1; ";
                string getUsers = "SELECT * FROM " + tableName; ;


                SqlCommand cmd = new SqlCommand(getUsers, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    User user = new User();
                   user.Id= Convert.ToInt32(dr["userid"]);
                   user.Group_Id = Convert.ToInt32(dr["group_id"]);
                   user.FullName = (string)(dr["fullname"]);
                   user.Mail = (string)dr["mail"];
                    user.Gender = (string)(dr["gender"]);
                   user.PrefDay1 = Convert.ToInt32(dr["prefday1"]);
                   //user.PrefDay2 = Convert.ToInt32(dr["prefday1"]);
                   user.PrefHour1 = Convert.ToInt32(dr["prefhour1"]);
                   user.PrefHour2 = Convert.ToInt32(dr["prefhour2"]);
                   user.Status= Convert.ToInt32(dr["family_status"]);
                   user.YearsOfEducation= Convert.ToInt32(dr["education"]);
                   user.Residence = Convert.ToInt32(dr["residence"]);
                    user.Group_Version = Convert.ToInt32(dr["Group_Version"]);
                    


                   allUsers.Add(user);
                }

                return allUsers;
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
            sb.AppendFormat(" Values({0},'{1}','{2}',{3},{4},{5},{6}); ", appClass.Id,appClass.Description,appClass.Title,appClass.Status,appClass.Position,appClass.Score,appClass.Version);
            String prefix = "INSERT INTO Class " + "( class_id,class_desc,class_title,class_status,approved_class_position,score,class_version)";
            command = prefix + sb.ToString();

            return command;
        }

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
                string getClasses = "SELECT * FROM " + tableName +" where class_version = (select max(class_version) from "+ tableName+");" ;
                    
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
                    appClass.Version = Convert.ToInt32(dr["class_version"]);
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
        /*******************************Get All Sections From DB***************************************/
        /*************************************************************************************************/

        public List<Section> GetAllSectionFromDB(string tableName,string connectionString)
        {
            List<Section> allSection = new List<Section>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getClasses = "SELECT *  from " + tableName+ " where class_version = (select max(class_version) FROM section)";

                SqlCommand cmd = new SqlCommand(getClasses, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Section sectionClass = new Section();
                    sectionClass.Id = Convert.ToInt32(dr["section_id"]);
                    sectionClass.Description = (string)(dr["section_desc"]);
                    sectionClass.Title = (string)dr["section_title"];
                    sectionClass.Status = Convert.ToInt32(dr["section_status"]);
                    sectionClass.Position = Convert.ToInt32(dr["approved_section_position"]);
                    sectionClass.Version = Convert.ToInt32(dr["class_version"]);
                    sectionClass.ClassId = Convert.ToInt32(dr["class_id"]);
                    sectionClass.HasFeedback = Convert.ToInt32(dr["has_feedback"]);
                    sectionClass.FilePath = (string)(dr["file_path"]);
                    //appClass.CreationDate = Convert.ToDateTime(dr["class_timestamp"]);
                    allSection.Add(sectionClass);
                }

                return allSection;
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
        /*******************************Get Last Class ID From DB******************************************/
        /*************************************************************************************************/
        public AppClass GetLastId(string tableName,string connectionString)
        {
            AppClass appClass = new AppClass();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getClasses = "SELECT max(class_id) as 'max_id', max(class_version) as max_version FROM " + tableName;
                
                SqlCommand cmd = new SqlCommand(getClasses, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    
                    appClass.Id = Convert.ToInt32(dr["max_id"]);
                    appClass.Version = Convert.ToInt32(dr["max_version"]);

                }

                return appClass;
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

      
        public Section GetLastSectionId(string tableName, string connectionString)
        {
            Section section = new Section();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getClasses = "SELECT max(section_id) as 'max_id' FROM " + tableName;

                SqlCommand cmd = new SqlCommand(getClasses, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row


                    section.Id = Convert.ToInt32(dr["max_id"]);
                    

                }

                return section;
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
        /*******************************Insert  Class Array with new version*******************************/
        /*************************************************************************************************/
        public int InsertNewClassArray(List<AppClass> appClasses,string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            String cStr = "";
            try
            {
                con = connect(connectionString); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            for (int i = 0; i < appClasses.Count; i++)
            {
                cStr += BuildInsertCommandClass(appClasses[i]);
            }
           

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


        /**************************************************************************************************/
        /**********************************Insert Sections Array*******************************************/
        /**************************************************************************************************/

        public int InsertNewSessionsToDB(List<Section> sections,string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            String cStr = "";
            try
            {
                con = connect(connectionString); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            for (int i = 0; i < sections.Count; i++)
            {
                cStr += BuildInsertCommandSection(sections[i]);
            }


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

        private String BuildInsertCommandSection(Section section)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values({0},'{1}','{2}',{3},{4},{5},{6},{7},'{8}'); ", section.Id,section.Description,section.Title,section.Status,section.Position,section.ClassId,section.HasFeedback,section.Version,section.FilePath);
            String prefix = "INSERT INTO Section " + "( section_id,section_desc,section_title,section_status,approved_section_position,class_id,has_feedback,class_version,file_path)";
            command = prefix + sb.ToString();

            return command;
        }

        /**************************************************************************************************/
        /************************************Get All Cities From DB****************************************/
        /**************************************************************************************************/
        public List<City> GetAllCitiesFromDB(string tableName,string connectionString)
        {
            List<City> allCities = new List<City>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getCities = "SELECT *  from " + tableName;

                SqlCommand cmd = new SqlCommand(getCities, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    City city = new City();
                    city.Id= Convert.ToInt32(dr["id"]);
                    city.CityName = (string)(dr["Name"]);
                    
                    allCities.Add(city);
                }

                return allCities;
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
        /************************************Get All Groups From DB****************************************/
        /**************************************************************************************************/

        public List<Group> GetAllGroupsFromDB(int day,int grouptime,int education, string tableName, string connectionString)
        {
            List<Group> allGroups = new List<Group>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getGroups = "SELECT *  from " + tableName+" WHERE day1="+day+" AND hour1="+grouptime+" AND education="+education+ " AND group_version=(select max(group_version) FROM class_group  WHERE day1=" + day + " AND hour1=" + grouptime + " AND education=" + education + " )";

                SqlCommand cmd = new SqlCommand(getGroups, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Group group = new Group();
                    group.Group_Id = Convert.ToInt32(dr["group_id"]);
                     group.Group_Name= (string)(dr["group_name"]);
                    group.Group_Version = Convert.ToInt32(dr["group_version"]);
                    group.Hour1 = Convert.ToInt32(dr["hour1"]);
                    group.Max_Partcipants = Convert.ToInt32(dr["max_participants"]);
                    group.Num_Of_Registered = Convert.ToInt32(dr["num_of_registered"]);
                    group.Education = Convert.ToInt32(dr["education"]);
                    group.Class_Version = Convert.ToInt32(dr["class_version"]);
                    group.Day1 = Convert.ToInt32(dr["day1"]);                   
                    group.IsStarted = Convert.ToBoolean(dr["IsStarted"]);
                    group.IsFinished = Convert.ToBoolean(dr["IsFinished"]);

                    allGroups.Add(group);
                }

                return allGroups;
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
        /***********************************Insert a New Group Into DB*************************************/
        /**************************************************************************************************/

        public int InsertNewGroupToDB(Group group)
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

            String cStr = BuildInsertCommandNewGroup(group);      // helper method to build the insert string

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

        /**************************************************************************************************/
        /*******************************Update Group Participant In DB*************************************/
        /**************************************************************************************************/

        public int UpdateGroupParticipant(Group group,string tableName,string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = connect(connectionString);
            String cStr = BuildUpdateDetailsCommand(group, tableName);
            cmd = CreateCommand(cStr, con);

            try
            { 
                int numAffected = cmd.ExecuteNonQuery();
                return numAffected;

            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }

            finally
            {
                if (con != null)
                { con.Close(); }
            }
        }

        public string BuildUpdateDetailsCommand(Group group, string tableName)
        {
            int currentNum = group.Num_Of_Registered+1;
            int group_id = group.Group_Id;
            int group_version = group.Group_Version;

            string updateComand = "UPDATE " + tableName+" ";
            updateComand += "set num_of_registered=" + currentNum + " where group_id=" + group_id+" AND group_version="+group_version;
            return updateComand;
        }

        private String BuildInsertCommandNewGroup(Group group)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values({0},'{1}',{2},{3},{4},{5},{6},{7},{8}); ", group.Group_Id,group.Group_Name,group.Day1,group.Hour1,group.Max_Partcipants,group.Num_Of_Registered,group.Group_Version,group.Education,group.Class_Version);
            String prefix = "INSERT INTO class_group " + "( Group_Id,Group_Name,Day1,Hour1,Max_Participants,Num_Of_Registered,Group_Version,Education,Class_Version)";
            command = prefix + sb.ToString();

            return command;
        }

        /**************************************************************************************************/
        /*******************************Receiving Admin Credentials From DB********************************/
        /**************************************************************************************************/

        public Admin AdminAuthentication(Admin admin,string tableName,string connectionString)
        {


            Admin user = new Admin();
            SqlConnection con = null;
            try
            {
                con = connect(connectionString);
                string getQuery = "SELECT * FROM " + tableName + " WHERE admin_userName='" + admin.Admin_UserName + "'";

                SqlCommand cmd = new SqlCommand(getQuery, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {   // Read till the end of the data into a row

                        admin.Admin_UserName = (string)(dr["admin_userName"]);
                        admin.Admin_Password = (string)(dr["admin_password"]);

                    }

                    return admin;
                }
                else
                {
                    admin.Found = false;
                    return admin;
                }
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
        /*******************************Retrive All Groups From DB*****************************************/
        /**************************************************************************************************/

        public List<Group> GetAllGroupsFromDB(string tableName, string connectionString)
        {
            List<Group> allGroups = new List<Group>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getGroups = "SELECT *  from " + tableName;

                SqlCommand cmd = new SqlCommand(getGroups, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Group group = new Group();
                    group.Group_Id = Convert.ToInt32(dr["group_id"]);
                    group.Group_Name = (string)(dr["group_name"]);
                    group.Group_Version = Convert.ToInt32(dr["group_version"]);
                    group.Hour1 = Convert.ToInt32(dr["hour1"]);
                    group.Max_Partcipants = Convert.ToInt32(dr["max_participants"]);
                    group.Num_Of_Registered = Convert.ToInt32(dr["num_of_registered"]);
                    group.Education = Convert.ToInt32(dr["education"]);
                    group.Class_Version = Convert.ToInt32(dr["class_version"]);
                    group.Day1 = Convert.ToInt32(dr["day1"]);
                    group.IsStarted = Convert.ToBoolean(dr["isstarted"]);
                    group.IsFinished = Convert.ToBoolean(dr["isFinished"]);

                    allGroups.Add(group);
                }

                return allGroups;
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
        /*******************************Retrive All User In Class From DB**********************************/
        /**************************************************************************************************/

        public List<UserInClass> GetAllUsersInClassFromDb(string tableName, string connectionString)
        {
            string startTime;
            string endTime;
          
            
            List<UserInClass> allUserInClass = new List<UserInClass>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getGroups = "SELECT *  from " + tableName;

                SqlCommand cmd = new SqlCommand(getGroups, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    UserInClass userInClass = new UserInClass();
                    userInClass.UserId = Convert.ToInt32(dr["UserId"]);
                    userInClass.ClassId = Convert.ToInt32(dr["ClassId"]);
                    userInClass.ClassVersion = Convert.ToInt32(dr["ClassVersion"]);
                    startTime = dr["startTime"].ToString();
                    endTime = dr["endTime"].ToString();
                    userInClass.StartTime= DateTime.Parse(startTime);
                    userInClass.EndTime=DateTime.Parse(startTime);
                   

                    allUserInClass.Add(userInClass);
                }

                return allUserInClass;
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

        /*************************************************************************************************/
        /*************************************Create Sql Command******************************************/
        /************************************************************************************************/
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        /************************************************************************************************/
        /******************************Create Connection************************************************/
        /***********************************************************************************************/
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
     
    }
}