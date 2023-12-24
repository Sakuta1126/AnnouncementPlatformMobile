using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementPlatformMobile
{
    public static class UserStore
    {
        public static int LoggedInUserId { get; private set; }
        public static bool LoggedInUserAdmin { get; private set; }
        public static bool islogged { get; private set; }
       
        public static void SetLoggedInUserId(int userId)
        {
            LoggedInUserId = userId;
        } 
        public static void SetLoggedInUserAdmin(bool userIsAdmin)
        {
            LoggedInUserAdmin = userIsAdmin;
        }
        public static void SetLoggedInUser(bool userIsLogged)
        {
            islogged = userIsLogged;
        }
    }
}
