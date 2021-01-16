using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CLC_MinesweeperMVC.Models {
    public class UserModel {
        // Properties for the User 
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public int zipcode { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class UsersDBContext:DbContext {
        public DbSet<UserModel> Users { get; set; }
    }
}