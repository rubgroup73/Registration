using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class Admin
    {
        bool newUserName;
        bool newEmail;

        public int Admin_Id { get; set; }
        public string Admin_Firsname { get; set; }
        public string Admin_LastName { get; set; }
        public string Admin_Email { get; set; }
        public string Admin_UserName { get; set; }
        public string Admin_Password { get; set; }
        public bool Found { get; set; }
        public int IsManeger { get; set; }

        public Admin(int admin_id, string admin_firsname, string admin_lastName, string admin_email, string admin_userName, string admin_password,bool found,int isManeger=0)
        {
            Admin_Id = admin_id;
            Admin_Firsname = admin_firsname;
            Admin_LastName = admin_lastName;
            Admin_Email = admin_email;
            Admin_UserName = admin_userName;
            Admin_Password = admin_password;
            Found = found;
            IsManeger = isManeger;
        }
        public Admin()
        {

        }

        public int AddNewAdmin(Admin admin)
        {
            DBservices db = new DBservices();
            return db.AddNewAdmin(admin, "admin_class", "ConnectionStringPerson");
        }

        public bool GetAllAdminsFromDb(string username, string email)
        {
            DBservices db = new DBservices();
            List<Admin> adminsDb = db.GetAllAdminsFromDb("admin_class", "ConnectionStringPerson");
            return CheckeIfExists(adminsDb, username, email);
        }

       
        public bool CheckeIfExists(List<Admin> adminsDb, string username,string email)
        {
            
            for (int i = 0; i < adminsDb.Count; i++)
            {
                if(adminsDb[i].Admin_UserName == username)
                {
                    newUserName = true;
                }
            }
            for (int i = 0; i < adminsDb.Count; i++)
            {
                if (adminsDb[i].Admin_Email == email)
                {
                    newEmail = true;
                }
            }
            if (newUserName == true || newUserName == true)
                return true;
            else
                return false;
        }

        /**************************************************/
        /***********Authenticated an Admin*****************/
        public bool AdminAuthentication(Admin admin)
        {
            DBservices db = new DBservices();
            Admin adminFromDb = new Admin();
            
            adminFromDb = db.AdminAuthentication(admin, "admin_class", "ConnectionStringPerson");
            if (adminFromDb.Found == false)
                return false;
            else
                return AdminCredentials(admin, adminFromDb);
        }

        public bool AdminCredentials(Admin admin,Admin adminFromDb)
        {
            if (admin.Admin_Password == adminFromDb.Admin_Password && admin.Admin_UserName == adminFromDb.Admin_UserName)
                return true;
            else
                return false;
        }

       
    }
}