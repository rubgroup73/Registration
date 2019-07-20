using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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
                int userId = (int)cmd.ExecuteScalar(); // execute the command
                return userId;
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
                    user.Id = Convert.ToInt32(dr["userid"]);
                    user.UserName = (string)(dr["UserName"]);
                    user.Password = (string)(dr["User_password"]);
                    user.FullName = (string)(dr["fullname"]);
                    user.Group_Id = Convert.ToInt32(dr["group_id"]);
                    user.Group_Version = Convert.ToInt32(dr["group_version"]);
                    user.Token = (string)(dr["Token"]);

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
       

        private String BuildInsertCommand(User user)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}',{9},{10},{11},{12},{13},{14},{15},{16})", user.FullName, user.Gender.ToString(), user.BirthDate, user.Status, user.YearsOfEducation, user.UserName, user.Password, user.Mail, user.Phone, user.Residence, user.City, user.PrefDay1, user.PrefHour1, user.PrefHour2, user.Score,user.Group_Id,user.Group_Version);
            String prefix = "INSERT INTO AppUser " + "( FullName, Gender,Birthday,Family_Status ,Education, UserName ,User_Password ,Mail,phone,Residence,City,prefday1,prefhour1,prefhour2,score,group_id,group_version) output INSERTED.userid";
            command = prefix + sb.ToString();

            return command;
        }
        /**************************************************************************************************/
        /*******************************Insert Class*******************************************************/
       
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
        /*******************************Insert new Class - Add new class*******************************************************/
 
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

                if (numEffected == 1)
                {
                    con.Close();
                    InsertHomeWorkToDB(appClass.HomeWork,1);//1 = not upgrade version
                
                }
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

        public int InsertHomeWorkToDB(HomeWork homeWork,int upgradeVer)
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

            String cStr = BuildInsertCommandHomeWork(homeWork,upgradeVer);      // **

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the comman
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
            sb.AppendFormat(" Values({0},'{1}','{2}',{3},{4},{5},{6},'{7}'); ", appClass.Id,appClass.Description,appClass.Title,appClass.Status,appClass.Position,appClass.Score,appClass.Version,appClass.Class_File_Path);
            String prefix = "INSERT INTO Class " + "( class_id,class_desc,class_title,class_status,approved_class_position,score,class_version,class_file_path)";
            command = prefix + sb.ToString();

            return command;
        }
       

        /*******************************Insert Homewok********************************************/
        private String BuildInsertCommandHomeWork(HomeWork homework,int upgradeVer)//**
        {
            String command;
            if (upgradeVer == 2)
            {
                homework.Class_Version += 1;
            }
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values({0},{1},'{2}','{3}','{4}','{5}'); ", homework.Class_Id, homework.Class_Version, homework.Homework_Name, homework.Homework_Desc, homework.Homework_Image, homework.Homework_Audio);
            String prefix = "INSERT INTO homework " + "( class_id,Class_Version,Homework_Name,Homework_Desc ,Homework_Image,Homework_Audio)";
            command = prefix + sb.ToString();

            return command;
        }

        /**************************************************************************************************/
        /*******************************Get All Classes From DB********************************************/

        public List<AppClass> GetAllClassFromDB(string tableName, string connectionString)
        {
             
            List<AppClass> allClass = new List<AppClass>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                                                 //string getClasses2 = "SELECT * FROM " + tableName +" where class_version = (select max(class_version) from "+ tableName+");" ;

                string getClasses= "SELECT* FROM class inner join homework on Class.class_id=HomeWork.class_id and class.class_version=HomeWork.class_version where class.class_version = (select max(class_version) from class);";
                    
                SqlCommand cmd = new SqlCommand(getClasses, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
             
                    AppClass appClass = new AppClass();
                    appClass.HomeWork = new HomeWork();
                    appClass.HomeWork.Class_Id= Convert.ToInt32(dr["class_id"]);
                    appClass.HomeWork.Class_Version= Convert.ToInt32(dr["class_version"]);
                    appClass.HomeWork.Homework_Desc= (string)dr["homework_desc"];
                    appClass.HomeWork.Homework_Image= (string)dr["homework_image"];
                    appClass.HomeWork.Homework_Audio= (string)dr["homework_audio"];
                    appClass.HomeWork.Homework_Name = (string)dr["homework_name"];
                    appClass.Id = Convert.ToInt32(dr["class_id"]);
                    appClass.Description = (string)(dr["class_desc"]);
                    appClass.Title = (string)dr["class_title"];
                    appClass.Status = Convert.ToInt32(dr["class_status"]);
                    appClass.Position = Convert.ToInt32(dr["approved_class_position"]);
                    appClass.Score = Convert.ToInt32(dr["score"]);
                    appClass.Version = Convert.ToInt32(dr["class_version"]);
                    appClass.Class_File_Path= (string)dr["Class_File_Path"];
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
                if(con != null)
                {con.Close();}
                int numEffected2 = InsertNewHomeWorkArray(appClasses, connectionString);
                return numEffected2;
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
        //*********************************************************************************
        //************Insert Class Array with new version*****
        public int InsertNewClassArray2(List<AppClass> appClasses, string connectionString)
        {
            int numEffected = 0;
            int numEffected2 = 0;
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
                numEffected = cmd.ExecuteNonQuery(); // execute the command
                if (numEffected == 1)
                {
                    con.Close();
                    numEffected2 = InsertHomeWorkToDB(appClasses[0].HomeWork,2);//2= upgrade version
                    
                }
                return numEffected2;

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

        //*
        //*insert updated homework version after push the "shmor shinueem"
        public int InsertNewHomeWorkArray(List<AppClass> appClasses, string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            String cStr = "";
            try
            {
                con = connect(connectionString); // create the connection
            }
            catch (Exception ex)
            {throw (ex); }
            for (int i = 0; i < appClasses.Count; i++)
            {
                cStr += BuildInsertCommandHomeWork(appClasses[i].HomeWork,2);//**
            }
            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {throw (ex);}

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
                int numEffected = cmd.ExecuteNonQuery();
                
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
        /************************************Get All Groups From DB a Specific Group***********************/
        

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
       

        public int InsertNewGroupToDB(Group group,DateTime next)
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

            String cStr = BuildInsertCommandNewGroup(group, next);      // helper method to build the insert string

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
            string updateComand;

            updateComand = "UPDATE " + tableName+" ";
            updateComand += "set num_of_registered=" + currentNum + " where group_id=" + group_id + " AND group_version=" + group_version;
            
            
            return updateComand;
        }

        private String BuildInsertCommandNewGroup(Group group, DateTime next)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values({0},'{1}',{2},{3},{4},{5},{6},{7},{8},'{9}'); ", group.Group_Id,group.Group_Name,group.Day1,group.Hour1,group.Max_Partcipants,group.Num_Of_Registered,group.Group_Version,group.Education,group.Class_Version, next.ToString());
            String prefix = "INSERT INTO class_group " + "( Group_Id,Group_Name,Day1,Hour1,Max_Participants,Num_Of_Registered,Group_Version,Education,Class_Version,start_time)";
            command = prefix + sb.ToString();

            return command;
        }

        /**************************************************************************************************/
        /*******************************Receiving Admin Credentials From DB********************************/
       
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

                        user.Admin_UserName = (string)(dr["admin_userName"]);
                        user.Admin_Password = (string)(dr["admin_password"]);
                        
                        user.Found = true;

                    }

                    return user;
                }
                else
                {
                    user.Found = false;
                    return user;
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
        /*************************************Add New Admin To DB******************************************/
      
        public int AddNewAdmin(Admin admin,string tableName,string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect(connectionString); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommandAddAdmin(admin);      // helper method to build the insert string

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

        private String BuildInsertCommandAddAdmin(Admin admin)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values('{0}','{1}','{2}','{3}','{4}')", admin.Admin_Firsname,admin.Admin_LastName,admin.Admin_Email,admin.Admin_UserName,admin.Admin_Password);
            String prefix = "INSERT INTO admin_class " + "( admin_firstName,admin_lastName,admin_email,admin_userName,admin_password)";
            command = prefix + sb.ToString();

            return command;
        }

        /**************************************************************************************************/
        /*************************************Get All Admins To DB*****************************************/

        public List<Admin> GetAllAdminsFromDb(string tableName, string connectionString)
        {
            List<Admin> allAdmins = new List<Admin>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getAdmins = "SELECT *  from " + tableName;

                SqlCommand cmd = new SqlCommand(getAdmins, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Admin admin = new Admin();
                    admin.Admin_Firsname = (string)(dr["admin_firstName"]);
                    admin.Admin_LastName = (string)(dr["admin_lastName"]);
                    admin.Admin_Email = (string)(dr["admin_email"]);
                    admin.Admin_UserName = (string)(dr["admin_userName"]);
                    admin.IsManeger = Convert.ToInt32(dr["isManager"]);

                    allAdmins.Add(admin);
                }

                return allAdmins;
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
        /*******************************Return All Groups From DB*****************************************/

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
        /*******************************Return All User In Class From DB**********************************/

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

        /**************************************************************************************************/
        /*******************************Return All Courses From DB*****************************************/
        
            public List<Course> GetCoursesFromDB(string tableName,string connectionString)
        {
            string classDateCreated;
            List<Course> courses = new List<Course>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getCourses = "SELECT *  from " + tableName+ " order by date_created";

                SqlCommand cmd = new SqlCommand(getCourses, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Course course = new Course();
                    course.Course_Version = Convert.ToInt32(dr["Course_Version"]);
                    course.Course_Name = (string)(dr["Course_Name"]);
                    classDateCreated = dr["Date_Created"].ToString();
                    course.Date_Created = DateTime.Parse(classDateCreated);
                    courses.Add(course);
                }

                return courses;
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
        /*******************************Return Top 5 Cities From DB*****************************************/

        public List<City> GetAllCitiesFromDB(string connectionString)
        {
            List<City> topFive = new List<City>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getTopCities = "select top 5 count (*) as 'NumOfRegistered', city, cities.Name from appuser inner join cities on cities.id=AppUser.City group by appuser.City, cities.Name order by NumOfRegistered desc;";

                SqlCommand cmd = new SqlCommand(getTopCities, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    City city = new City();
                    city.Id = Convert.ToInt32(dr["city"]);
                    city.CityName = (string)(dr["Name"]);
                    city.NumOfUsers = Convert.ToInt32(dr["NumOfRegistered"]);

                    topFive.Add(city);
                }

                return topFive;
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
        /*********************Return All Users Per Education From DB**************************************/

        public List<User> GetAllUsersPerEducationFromDb(string connectionString)
        {
            List<User> users = new List<User>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getRegisteredPerUser = "select  count (*) as 'NumOfRegistered', Education, Education.education_name from AppUser inner join Education on Education.id=AppUser.Education group by appuser.Education, Education.education_name order by NumOfRegistered desc;";

                SqlCommand cmd = new SqlCommand(getRegisteredPerUser, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    User user = new User((string)(dr["education_name"]), Convert.ToInt32(dr["NumOfRegistered"]));
                    users.Add(user);
                }

                return users;
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
        /*********************Return A Specific Group Users From DB***************************************/

        public Group GetAllGroupsFromDbVer2(string tableName,string connectionString, int prefday, int prefhour, int education,DateTime next)
        {
            Group group = new Group();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getGroup = "SELECT *  from " + tableName + " WHERE day1=" + prefday + " AND hour1=" + prefhour + " AND education=" + education + " AND group_version=(select max(group_version) FROM class_group  WHERE day1=" + prefday + " AND hour1=" + prefhour + " AND education=" + education + " )";

                SqlCommand cmd = new SqlCommand(getGroup, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    
                    group.Group_Id = Convert.ToInt32(dr["group_id"]);
                    group.Group_Name = (string)(dr["group_name"]);
                    group.Group_Version = Convert.ToInt32(dr["group_version"]);
                    group.Hour1 = Convert.ToInt32(dr["hour1"]);
                    group.Max_Partcipants = Convert.ToInt32(dr["max_participants"]);
                    group.Num_Of_Registered = Convert.ToInt32(dr["num_of_registered"]);
                    group.Education = Convert.ToInt32(dr["education"]);
                    group.Class_Version = Convert.ToInt32(dr["class_version"]);
                    group.Day1 = Convert.ToInt32(dr["day1"]);
                    group.IsStarted = Convert.ToBoolean(dr["IsStarted"]);
                    group.IsFinished = Convert.ToBoolean(dr["IsFinished"]);
                    group.Start_Time = DateTime.Parse((dr["Start_Time"]).ToString());

                   
                }

                var groupStartTime = group.Start_Time.Date.ToString("MM/dd/yyyy");
                var parameterDate = DateTime.ParseExact(groupStartTime, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var todaysDate = DateTime.Today;

                //if (groupStartTimeY >= currentDayY && groupStartTimeM >= currentDayM)
                //    createNewGroup = false;

                if (group.Start_Time.Year == 1900)
                {
                    group.Start_Time = next;
                }
                
                if (group.Max_Partcipants > group.Num_Of_Registered&& parameterDate>= todaysDate)
                    return group;
                else
                {
                    con.Close();
                    int maxClassVersion=CheckmaxClassVersion(connectionString);
                    group.Class_Version = maxClassVersion;
                    group.Group_Version += 1;
                    group.Num_Of_Registered = 0;
                    con.Close();
                    InsertNewGroupToDB(group,next);

                    return group;

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
        //**************************************************************************************************//
        //**************************************Getting the max class version********************************//
        //**************************************************************************************************//
        private int CheckmaxClassVersion(string connectionString)
        {

            int max_version=0;
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getMax = "select max(class_version) as maxVersion from class;";

                SqlCommand cmd = new SqlCommand(getMax, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {  
                    max_version = Convert.ToInt32(dr["maxVersion"]);  
                }

                con.Close();
                return max_version;
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
        //**************************************************************************************************//
        //**************************************************************************************************//
        //**************************************************************************************************//
        public int InsertToGroup(User user,string userTableName,string groupTableName,string connectionString)
        {    
                SqlConnection con;
                SqlCommand cmd;

                try
                {
                    con = connect(connectionString); // create the connection
                }
                catch (Exception ex)
                {
                    // write to log
                    throw (ex);
                }

                String cStr = BuildInsertCommand(user);      // helper method to build the insert string

                cmd = CreateCommand(cStr, con);             // create the command
                int userId;
                try
                {
                userId = (int)cmd.ExecuteScalar();
                   
                if (userId != 0)
                {
                    con.Close();
                    UpdateGroupParticipant(user.Group, groupTableName, connectionString);
                    
                }
                return userId;
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

        /********************************************REACT*************************************************/
        /*****************************Getting specific user's (and group) class version for react***********/
        /***************************************************************************************************/
        public void InserNewUserInClass(int userId)
        {
            DateTime start_time=DateTime.Now;
            int classVersion=0;
            List<AppClass> userInClass = new List<AppClass>();
            SqlConnection con = null;
            try
            {

                con = connect("ConnectionStringPerson"); // create a connection to the database using the connection String defined in the web config file
                string getClassVersion = "select class_version,start_time from class_group where group_version = (select group_version from appuser where userid = " + userId + ") and group_id = (select group_id from appuser where userid = " + userId + ")";

                SqlCommand cmd = new SqlCommand(getClassVersion, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    classVersion = Convert.ToInt32(dr["class_version"]);
                    start_time = DateTime.Parse(dr["start_time"].ToString());
                   
                }

                if (classVersion != 0)
                {
                    con.Close();
                    userInClass = GetClassesOfUser(classVersion, "ConnectionStringPerson");
                    InsertNewUserInClass2(userId, classVersion, userInClass,start_time, "userinclass", "ConnectionStringPerson");
                    InsertNewUserInHomeWork2(userId, classVersion, userInClass, start_time, "userinclass", "ConnectionStringPerson");
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
        //*
        //*Insert User In classes for new user
        //*
        public int InsertNewUserInClass2(int userId, int classVersion,List<AppClass> classesOfVersion, DateTime start_time,string tableName,string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {con = connect("ConnectionStringPerson");}
            catch (Exception ex)
            {throw (ex);}

            String cStr = BuildInsertUserInClass(classesOfVersion, userId,start_time,2);
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                int numEffected2 = 0;
                if (numEffected != 0)
                {
                    con.Close();
                    //Get list of section for the classes of this version
                    List<Section> userSections= GetSectionsList(classesOfVersion, connectionString);
                    //Insert UserInSection for the sections above
                    numEffected2= InsertNewUserInSection(userId, userSections, "UserInClass", connectionString);
                }
                return numEffected2;
            }
            catch (Exception ex)
            {throw (ex);}
           
            finally
            {
                if (con != null)
                { con.Close();}
            }

        }
        //*
        //*Insert User In HomeWork for new user
        //*
        public int InsertNewUserInHomeWork2(int userId, int classVersion, List<AppClass> classesOfVersion, DateTime start_time, string tableName, string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {con = connect("ConnectionStringPerson"); }
            catch (Exception ex)
            {throw (ex);}

            String cStr = BuildInsertUserInClass(classesOfVersion, userId, start_time,1); 
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command            
                return numEffected;
            }
            catch (Exception ex)
            {throw (ex);}

            finally
            {
                if (con != null)
                {con.Close();}
            }

        }

        //**
        //GetSectionsList
        //**
        public List<Section> GetSectionsList(List<AppClass> classes,string connectionString)
        {
            int class_version = classes[0].Version;
          
            List<Section> allSection = new List<Section>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getSections = "select * from section as s inner join Class as c on s.class_id = c.class_id and c.class_version = s.class_version where c.class_version ="+ class_version+" and c.class_status = 4 and s.section_status = 4 order by c.class_id asc, s.approved_section_position;";

                          SqlCommand cmd = new SqlCommand(getSections, con);
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
        public int InsertNewUserInSection(int userId,List<Section> userSections,string tableName,string connectionString)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect(connectionString); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertUserInSection(userSections, userId);      // helper method to build the insert string

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
        //*********************************** BuildInsertUserInSection*****************************************************************//
        private string BuildInsertUserInSection(List<Section> userSections,int userId)
        { Section section = new Section() ;
            string str = "";
            for (int i = 0; i < userSections.Count; i++)
            {
                section = userSections[i];
             str +=   "Insert into userinsection(section_id, class_id, class_version, userId) values("+ section.Id+ ","+section.ClassId+","+section.Version+","+userId+");";
            }
            return str;
        }

        //*****************************************************************************************************************************// 
        //**********************Getting all classes from a specific version*************************************************************// 

        public List<AppClass> GetClassesOfUser(int classVersion, string connectionString)
        {
            List<AppClass> allClass = new List<AppClass>();
            SqlConnection con = null;
            try
            {
                //**********************Getting all classes from a specific version***********// 
                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getClasses = "select * from class where class_version="+classVersion + "and class_status=4 order by approved_class_position asc";

                SqlCommand cmd = new SqlCommand(getClasses, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    AppClass appClass = new AppClass();
                    appClass.Id = Convert.ToInt32(dr["class_id"]);
                    appClass.Description = (string)(dr["class_desc"]);
                    appClass.Title = (string)dr["class_title"];
                    appClass.Status = Convert.ToInt32(dr["class_status"]);           
                    appClass.Score = Convert.ToInt32(dr["score"]);
                    appClass.Version = Convert.ToInt32(dr["class_version"]);
                    allClass.Add(appClass);
                }

                con.Close();
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

        /***************************************REACT***********************************************/
        /*************************************return group ID******************************************/
        /************************************************************************************************/
        public int GetClassVersionReact(int userId)
        {
            int classVersion = 0;
           
            SqlConnection con = null;
            try
            {

                con = connect("ConnectionStringPerson"); // create a connection to the database using the connection String defined in the web config file
                string getClassVersion = "select class_version from class_group where group_version = (select group_version from appuser where userid = " + userId + ") and group_id = (select group_id from appuser where userid = " + userId + ")";

                SqlCommand cmd = new SqlCommand(getClassVersion, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    classVersion = Convert.ToInt32(dr["class_version"]);

                }

                return classVersion;

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
        //******************************************REACT******************************************//
        //******************************Getting all section of user for react***********************//
        //***********************************************************************************************//

        public List<Section> GetAllSectionsReact(int classVersion,string tableName, string connectionString)
        {
            List<Section> allSection = new List<Section>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getSections = "SELECT * FROM " + tableName + " where class_version=" + classVersion+ "  order by class_id asc";

                SqlCommand cmd = new SqlCommand(getSections, con);
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
        //******************************************REACT******************************************//
        //******************************Getting all classes of user for react***********************//
        //***********************************************************************************************//
        public List<AppClass> GetAllClassesReact(int classVersion,string tableName,string connectionString)
        {

            List<AppClass> allClasses = new List<AppClass>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file

                string getClasses = "SELECT * FROM " + tableName + " where class_version=" + classVersion;


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
                    allClasses.Add(appClass);


                    
                }

                return allClasses;
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
        /*************************************BuildInsertUserInClass******************************************/
        /************************************************************************************************/

        private string BuildInsertUserInClass(List<AppClass> appClasses,int userId,DateTime start_time,int ClassOrHomeWork)
        {
            //int ClassOrHomeWork
            //HomeWork = 1
            //Class = 2
            int numOfDays = 6;
            String command = "";
           
            if (ClassOrHomeWork == 1)
            {
                for (int i = 0; i < appClasses.Count; i++)
                {
                    for (int j = 1; j <= numOfDays; j++)
                    {
                        command += "INSERT INTO userInHomeWork(homeworkid,userId,class_id,class_version,start_Time,end_Time,is_Started,is_Finished,should_start_time,user_feeling_start,user_feeling_finish) Values(";
                        command += j + "," + userId + "," + appClasses[i].Id + "," + appClasses[i].Version + ",'1900-01-01','1900-01-01',0,0,'" + start_time.AddDays(j).ToString() + "',-1,-1);";
                      
                    }
                    start_time = start_time.AddDays(7);

                }
                return command;
            }
            else
            {
                for (int i = 0; i < appClasses.Count; i++)
                {
                    command += "INSERT INTO UserInClass(userId,ClassId,classVersion,startTime,endTime,should_start_time) Values(" + userId + "," + appClasses[i].Id + "," + appClasses[i].Version + ",'1900-01-01','1900-01-01','" + start_time.ToString() + "');";
                    start_time = start_time.AddDays(7);
                }
                return command;
             
            }
        }


       
    //*******************************************************************************************************//
    //******************************************Getting all UserInClass for a Specific user - React**********//
    //*******************************************************************************************************//
    public List<UserInClass> GetUserInClassReact(int userId,string tableName,string connectionString)
        {
            string startTime;
            string endTime;


            List<UserInClass> allUserInClass = new List<UserInClass>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                string getUserInClass = "select * from class inner join UserInClass on class.class_version=UserInClass.ClassVersion and class.class_id=UserInClass.classId where class.class_status=4 and UserInClass.userId=" + userId+ " order by approved_class_position asc";
                    

                SqlCommand cmd = new SqlCommand(getUserInClass, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    UserInClass userInClass = new UserInClass();
                    AppClass appClass = new AppClass();
                    userInClass.UserId = Convert.ToInt32(dr["UserId"]);
                    userInClass.ClassId = Convert.ToInt32(dr["Class_Id"]);
                    userInClass.ClassVersion = Convert.ToInt32(dr["Class_Version"]);
                    userInClass.ClassPosition = Convert.ToInt32(dr["approved_class_position"]);
                    startTime = dr["startTime"].ToString();
                    endTime = dr["endTime"].ToString();
                    userInClass.StartTime = DateTime.Parse(endTime);
                    userInClass.EndTime = DateTime.Parse(startTime);
                    userInClass.IsStarted=Convert.ToBoolean(dr["isStarted"]);
                    userInClass.IsFinished = Convert.ToBoolean(dr["isFinished"]);
                    appClass.Description = (string)(dr["class_desc"]);
                    appClass.Status = Convert.ToInt32(dr["class_status"]);
                    appClass.Position = Convert.ToInt32(dr["approved_class_position"]);
                    appClass.Score = Convert.ToInt32(dr["score"]);
                    appClass.Version = Convert.ToInt32(dr["class_version"]);
                    appClass.Title = (string)(dr["class_title"]);
                    appClass.Id = Convert.ToInt32(dr["Class_Id"]);
                    appClass.Class_File_Path = (string)(dr["Class_File_Path"]);
                    userInClass.ShouldStart = DateTime.Parse(dr["should_Start_time"].ToString());

                    userInClass.AppClass = appClass;

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

        //**
        //**Return all user's sections instances from DB --> will call from react app
        //**
        public List<UserInSection> GetUserInSectionReact(int userId, int classVersion, int classId)
        {
            
            List<UserInSection> userInSections = new List<UserInSection>();
            SqlConnection con = null;
            try
            {

                con = connect("ConnectionStringPerson"); // create a connection to the database using the connection String defined in the web config file
                
                string getUserInSection = "select * from userInSection as uis inner join section as s  on s.section_id=uis.section_id and s.class_id= uis.class_id and s.class_version=uis.class_version ";
                getUserInSection += "inner join Class as c on s.class_id =c.class_id and s.class_version=c.class_version ";
                getUserInSection += "where uis.userid=" + userId + " AND uis.class_id=" + classId + " AND uis.class_version=" + classVersion + " and c.class_status=4 and s.section_status=4 order by c.approved_class_position asc,s.approved_section_position asc";
                SqlCommand cmd = new SqlCommand(getUserInSection, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    UserInSection userInSection = new UserInSection();
                    userInSection.Class_Id = Convert.ToInt32(dr["class_id"]);
                    userInSection.Section_Id = Convert.ToInt32(dr["Section_Id"]);
                    userInSection.Class_Version = Convert.ToInt32(dr["Class_Version"]);
                    userInSection.UserId = Convert.ToInt32(dr["UserId"]);
                    userInSection.Play_Clicks = Convert.ToInt32(dr["Play_Clicks"]);
                    userInSection.Pause_Clicks = Convert.ToInt32(dr["Pause_Clicks"]);
                    userInSection.Stop_Clicks = Convert.ToInt32(dr["Stop_Clicks"]);
                    userInSection.Backward_Clicks = Convert.ToInt32(dr["Backward_Clicks"]);
                    userInSection.Forward_Clicks = Convert.ToInt32(dr["Forward_Clicks"]);
                    userInSection.Mute_Clicks = Convert.ToInt32(dr["Mute_Clicks"]);
                    userInSection.Unmute_Click = Convert.ToInt32(dr["Unmute_Click"]);
                    userInSection.Section_Start_Time = DateTime.Parse(dr["Section_Start_Time"].ToString());
                    userInSection.Section_End_Time = DateTime.Parse(dr["Section_End_Time"].ToString());
                    userInSection.Section_Is_Started = Convert.ToBoolean(dr["Section_Is_Started"]);
                    userInSection.Section_Is_Finished = Convert.ToBoolean(dr["Section_Is_Finished"]);
                    userInSection.User_Last_Point = (string)(dr["User_Last_Point"]);
                    userInSection.Pause_Duration = (string)(dr["Pause_Duration"]);
                    userInSection.Section_Total_Duration = (string)(dr["Section_Total_Duration"]);
                    userInSection.File_Path = (string)(dr["File_Path"]);
                    userInSection.Section_Title = (string)(dr["Section_Title"]);
                    userInSection.Approved_Section_Position = Convert.ToInt32(dr["Approved_Section_Position"]);
                    userInSection.Repeat_Section_Counter = Convert.ToInt32(dr["Repeat_Section_Counter"]);

                    userInSections.Add(userInSection);
                }
                return userInSections;


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

        //**
        //**Return all user's HomeWork instances from DB --> will call from react app
        //**
        public List<UserInHomeWork> GetUserInHomeWorkReact(int userId, int classVersion, int classId)
        {

            List<UserInHomeWork> userInHomeWorks = new List<UserInHomeWork>();
            SqlConnection con = null;
            try
            {

                con = connect("ConnectionStringPerson"); // create a connection to the database using the connection String defined in the web config file

                string getUserInHomeWork = "SELECT* FROM userInHomeWork as uihw inner join HomeWork as hw on uihw.class_id = hw.class_id and uihw.class_version = hw.class_version";
                getUserInHomeWork += " where userId=" + userId + " and uihw.class_id=" + classId + " and uihw.class_version=" + classVersion;
                getUserInHomeWork += " order by uihw.class_id asc, uihw.homeworkid asc;";

                SqlCommand cmd = new SqlCommand(getUserInHomeWork, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    UserInHomeWork userInHomeWork = new UserInHomeWork();
                    userInHomeWork.Class_Id = Convert.ToInt32(dr["class_id"]);
                    userInHomeWork.HomeWorkId = Convert.ToInt32(dr["HomeWorkId"]);
                    userInHomeWork.Class_Version = Convert.ToInt32(dr["Class_Version"]);
                    userInHomeWork.UserId = Convert.ToInt32(dr["UserId"]);
                    userInHomeWork.Start_Time = DateTime.Parse(dr["start_time"].ToString());
                    userInHomeWork.End_Time = DateTime.Parse(dr["End_Time"].ToString());
                    userInHomeWork.Should_Start_Time = DateTime.Parse(dr["Should_Start_Time"].ToString());
                    userInHomeWork.Is_Started = Convert.ToBoolean(dr["Is_Started"]);
                    userInHomeWork.Is_Finished = Convert.ToBoolean(dr["Is_Finished"]);
                    userInHomeWork.User_Feeling_Start = Convert.ToInt32(dr["User_Feeling_Start"]);
                    userInHomeWork.User_Feeling_Finish = Convert.ToInt32(dr["User_Feeling_Finish"]);
                    userInHomeWork.HomeWork_Name= (string)(dr["HomeWork_Name"]);
                    userInHomeWork.HomeWork_Desc = (string)(dr["HomeWork_Desc"]);
                    userInHomeWork.HomeWork_Image = (string)(dr["HomeWork_Image"]);
                    userInHomeWork.HomeWork_Audio = (string)(dr["HomeWork_Audio"]);
 
                    userInHomeWorks.Add(userInHomeWork);
                }
                return userInHomeWorks;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {con.Close();}

            }
        }

        //**
        //Update UserInClass status from React
        //**
        public int UserFeelingsReact(int feeling,UserInClass userInClass,string tableName,string userFeelingForDB)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = connect("ConnectionStringPerson");
            String cStr = BuildUpdateFeelingCommand(feeling, userInClass, tableName, userFeelingForDB);
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

        public string BuildUpdateFeelingCommand(int feeling, UserInClass userInClass, string tableName,string userFeelingForDB)
        {
            
            string updateComand = "UPDATE " + tableName + " ";
            updateComand += "set "+ userFeelingForDB+"=" + feeling + " where userId=" + userInClass.UserId + " AND classId=" + userInClass.ClassId;
            updateComand += " AND classVersion =" + userInClass.ClassVersion;
            return updateComand;
        }

        //**
        //Update UserInClass status from React
        //**
        public int UpdateDataUserInClassReact(UserInSection userInSection,string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = connect(connectionString);
            String cStr = BuildUpdateUserDataCommand(userInSection,userInSection.UserId,userInSection.Class_Version,userInSection.Class_Id,userInSection.Section_Id);
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
        public string BuildUpdateUserDataCommand(UserInSection userInSection,int userId,int classVersion,int classId,int sectionId)
        {
            //var dateStr = "14:02";
            //var now = DateTime.Now;
            //var dateTime = DateTime.ParseExact(dateStr, "mm:ss", null, System.Globalization.DateTimeStyles.None);
            //var t = "";
            //if(dateTime.Second<10)
            //    t= dateTime.Minute + ":0" + dateTime.Second;
            //else
            //    t =  dateTime.Minute + ":" + dateTime.Second;


            string Section_End_Time = Convert.ToString(userInSection.Section_End_Time);
            string Section_Start_Time = Convert.ToString(userInSection.Section_Start_Time);
            
            string updateComand = "UPDATE userInSection";
            updateComand += " set Play_Clicks=" + userInSection.Play_Clicks + ",Pause_Clicks=" + userInSection.Pause_Clicks + ",Stop_Clicks=" + userInSection.Stop_Clicks;
            updateComand += ",Backward_Clicks=" + userInSection.Backward_Clicks + ",Forward_Clicks=" + userInSection.Forward_Clicks;
            updateComand += ",Mute_Clicks=" + userInSection.Mute_Clicks + ",Unmute_Click= " + userInSection.Unmute_Click + ",Section_Start_Time='" + Section_Start_Time;
            updateComand += "',Section_End_Time='" + Section_End_Time + "',Section_Is_Started=" + Convert.ToInt32(userInSection.Section_Is_Started);
            updateComand += ",Section_Is_Finished=" + Convert.ToInt32(userInSection.Section_Is_Finished) + ",Repeat_Section_Counter=" + userInSection.Repeat_Section_Counter;
            updateComand += ",User_Last_Point='" + userInSection.User_Last_Point.ToString() + "',Pause_Duration='" + userInSection.Pause_Duration.ToString();
            updateComand += "',Section_Total_Duration='" + userInSection.Section_Total_Duration.ToString();
            updateComand += "' WHERE UserId=" + userId + " AND Class_Version=" + classVersion + " AND Class_Id=" + classId + " AND Section_Id=" + sectionId;
            return updateComand;
        }

        public int UpdateDataUserRepeatSecReact(UserInSection userInSection, string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = connect(connectionString);
            String cStr = BuildUpdateUserRepeatSecCommand(userInSection, userInSection.UserId, userInSection.Class_Version, userInSection.Class_Id, userInSection.Section_Id);
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
        public string BuildUpdateUserRepeatSecCommand(UserInSection userInSection, int userId, int classVersion, int classId, int sectionId)
        {
           
            string updateComand = "UPDATE userInSection";
            updateComand += " set Repeat_Section_Counter=" + userInSection.Repeat_Section_Counter;
            updateComand += " WHERE UserId=" + userId + " AND Class_Version=" + classVersion + " AND Class_Id=" + classId + " AND Section_Id=" + sectionId;
            return updateComand;
        }
        //**
        //**update when user finish the class
        public int UpdateClassStatuscReact(UserInClass userInClass, string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = connect(connectionString);
            String cStr = BuildUpdateUserClassStatusCommand(userInClass,userInClass.UserId,userInClass.ClassVersion,userInClass.ClassId);
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
        public string BuildUpdateUserClassStatusCommand(UserInClass userInClass, int userId, int classVersion, int classId)
        {

            string updateComand = "UPDATE UserInClass";
            updateComand += " set StartTime='" + userInClass.StartTime + "',EndTime='" + userInClass.EndTime + "',IsFinished=" +Convert.ToInt32(userInClass.IsFinished);
            updateComand += " WHERE UserId=" + userId + " AND ClassId=" + classId + " AND ClassVersion=" + classVersion;
            return updateComand;
        }
        //**
        //**update when user start the class
        public int UpdateClassStartedReact(UserInClass userInClass, string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = connect(connectionString);
            String cStr = BuildUpdateClassStartedStatusCommand(userInClass, userInClass.UserId, userInClass.ClassVersion, userInClass.ClassId);
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
        public string BuildUpdateClassStartedStatusCommand(UserInClass userInClass, int userId, int classVersion, int classId)
        {
            userInClass.IsStarted = true;
            string updateComand = "UPDATE UserInClass";
            updateComand += " set IsStarted=" + Convert.ToInt32(userInClass.IsStarted);
            updateComand += " WHERE UserId=" + userId + " AND ClassId=" + classId + " AND ClassVersion=" + classVersion;
            return updateComand;
        }

        //******************************************REACT******************************************//
        //******************************Getting HomeWork of user for react***********************//
        //***********************************************************************************************//
        public UserInHomeWork GetUserInHomeWorkFromDb(int userId,string connectionString)
        {

            UserInHomeWork userInHomeWork = new UserInHomeWork();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file

                string getClasses = "select uihw.homeworkid,uihw.userId,uihw.class_id,uihw.class_version,uihw.start_Time,uihw.end_Time,uihw.is_Started,uihw.is_Finished,uihw.should_start_time" +
                    ",uihw.user_feeling_start,uihw.user_feeling_finish,hw.homework_name,hw.homework_desc,hw.homework_image,hw.homework_audio from userInHomeWork as uihw inner join homework as" +
                    " hw on uihw.class_id=hw.class_id and uihw.class_version=hw.class_version where " +
                    "userid="+userId+" and Convert(date,should_start_time)= Convert(date, getdate());";



                SqlCommand cmd = new SqlCommand(getClasses, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr.HasRows != false)
                {
                    while (dr.Read())
                    {   // Read till the end of the data into a row
                        userInHomeWork.IsHomeWork = true;
                        userInHomeWork.UserId = Convert.ToInt32(dr["userId"]);
                        userInHomeWork.Class_Id = Convert.ToInt32(dr["class_id"]);
                        userInHomeWork.Class_Version = Convert.ToInt32(dr["class_version"]);
                        userInHomeWork.HomeWork_Desc = (string)(dr["homework_desc"]);
                        userInHomeWork.HomeWork_Audio = (string)(dr["homework_audio"]);
                        userInHomeWork.HomeWork_Name = (string)dr["homework_name"];
                        userInHomeWork.HomeWork_Image = (string)dr["homework_image"];
                        userInHomeWork.Start_Time = DateTime.Parse(dr["start_Time"].ToString());
                        userInHomeWork.End_Time = DateTime.Parse(dr["end_Time"].ToString());
                        userInHomeWork.Is_Started = Convert.ToBoolean(dr["is_Started"]);
                        userInHomeWork.Is_Finished = Convert.ToBoolean(dr["is_Finished"]);
                        userInHomeWork.Should_Start_Time = DateTime.Parse(dr["should_start_time"].ToString());
                        userInHomeWork.User_Feeling_Start = Convert.ToInt32(dr["user_feeling_start"]);
                        userInHomeWork.User_Feeling_Finish = Convert.ToInt32(dr["user_feeling_finish"]);

                    }
                }
                else { userInHomeWork.IsHomeWork = false; }

                return userInHomeWork;
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

        //**
        //**update when user start the class
        public int UpdateHomeWorkFinishedReact(UserInHomeWork userInHomeWork, string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = connect(connectionString);
            String cStr = BuildUpdateClassStartedStatusCommand2(userInHomeWork, userInHomeWork.UserId, userInHomeWork.Class_Version, userInHomeWork.Class_Id,false);
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
        public int UpdateUserStartHomeWork(UserInHomeWork userInHomeWork, string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = connect(connectionString);
            String cStr = BuildUpdateClassStartedStatusCommand2(userInHomeWork, userInHomeWork.UserId, userInHomeWork.Class_Version, userInHomeWork.Class_Id, true);
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
        public string BuildUpdateClassStartedStatusCommand2(UserInHomeWork userInHomeWork, int userId, int Class_Version, int Class_Id, bool start)
        {
            if (start == true) {
                userInHomeWork.Is_Started = true;
            string updateComand = "UPDATE UserInHomeWork";
            updateComand += " set Is_Started=" + Convert.ToInt32(userInHomeWork.Is_Started);
            updateComand += " WHERE UserId=" + userId + " AND Class_Id=" + Class_Id + " AND Class_Version=" + Class_Version;
            return updateComand; }
        
        else{
                userInHomeWork.Is_Finished = true;
                string updateComand = "UPDATE UserInHomeWork";
                updateComand += " set Is_Finished=" + Convert.ToInt32(userInHomeWork.Is_Finished);
                updateComand += " WHERE UserId=" + userId + " AND Class_Id=" + Class_Id + " AND Class_Version=" + Class_Version;
                return updateComand;
            }
        }

        //**
        //**create token for user, when login first time in React
        public int UpdateUserStartHomeWork(string username, string token,string connectionString)
        {
            SqlConnection con;
            SqlCommand cmd;
            con = connect(connectionString);
            String cStr = "UPDATE AppUser SET token=" + token + " WHERE UserName=" + username;
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

        /*************************************************************************************************/
        /*****************************Get All Messages from DB React**************************************/
         
        public List<Messages> GetAllMessagesDb(int groupId,int groupVersion,string connectionString)
        {

            List<Messages> messages = new List<Messages>();
            SqlConnection con = null;
            try
            {

                con = connect(connectionString); // create a connection to the database using the connection String defined in the web config file
                //string getUsers = "SELECT * FROM " + tableName + " where group_id = -1; ";
                string getMessages = "SELECT * FROM group_chat as gc inner join appuser as au on gc.userId = au.UserID where gc.group_version =" + groupVersion + " and gc.group_id =" + groupId + " order by gc.SentDate desc;";


                SqlCommand cmd = new SqlCommand(getMessages, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Messages message = new Messages();
                    message.UserId = Convert.ToInt32(dr["userId"]);
                    message.Full_Name = (string)(dr["fullname"]);
                    message.Group_Id = Convert.ToInt32(dr["group_id"]);
                    message.Group_Version= Convert.ToInt32(dr["Group_Version"]);
                    message.SentDate = DateTime.Parse(dr["sentdate"].ToString());
                    message.Content= (string)(dr["Content"]);

                    messages.Add(message);
                }

                return messages;
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
        /*****************************React-Insert New Message**************************************/
        public int UpdateMessagesDb(Messages message)
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

            String cStr = "insert into Group_Chat(userId,group_id,group_version,SentDate,content) VALUES(" + message.UserId + "," + message.Group_Id + "," + message.Group_Version + ",'" + message.SentDate.ToString() + "','" + message.Content + "');";

            cmd = CreateCommand(cStr, con); // create the command

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