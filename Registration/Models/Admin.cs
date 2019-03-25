using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class Admin
    {
        public int Admin_Id { get; set; }
        public string Admin_Firsname { get; set; }
        public string Admin_LastName { get; set; }
        public string Admin_Email { get; set; }
        public string Admin_UserName { get; set; }
        public string Admin_Password { get; set; }
        public bool Found { get; set; }

        public Admin(int admin_id, string admin_firsname, string admin_lastName, string admin_email, string admin_userName, string admin_password,bool found)
        {
            Admin_Id = admin_id;
            Admin_Firsname = admin_firsname;
            Admin_LastName = admin_lastName;
            Admin_Email = admin_email;
            Admin_UserName = admin_userName;
            Admin_Password = admin_password;
            Found = found;
        }
        public Admin()
        {

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