﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MafCode.Models
{
    public class UserSignUpInfo
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}