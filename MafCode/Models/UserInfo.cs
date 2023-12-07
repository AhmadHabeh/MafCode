using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MafCode.Models
{
    public class UserInfo
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string UniqueID { get; set; }
        public string ItemName { get; set; }
        public string ItemDetail { get; set; }
        public string ItemID { get; set; }

        public string ItemDescription { get; set; }
        public UserMapDetails mapDetails { get; set; }
    }
}