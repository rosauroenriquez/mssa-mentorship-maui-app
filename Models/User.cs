using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.Models
{
    public class User
    {
        public string emailAddress { get; set; }
        public string hashedPassword { get; set; }

        public User(string emailAddress, string password)
        {
            this.emailAddress = emailAddress;
            this.hashedPassword = password;
        }
        
    }

    public class UserProfile
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LinkedInURL { get; set; }
        public string JobTitle { get; set; }

    }

    public class NewUser 
    {
        public string userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string linkedInURL { get; set; }
        public string jobTitle { get; set; }
        public UserCredentials userCredentials { get; set; }

    }

    public class UserCredentials
    {
        public string userID { get; set; }
        public string emailAddress { get; set; }
        public string hashedPassword { get; set; }
        public string role { get; set; }
        public string accountStatus { get; set; }
        public bool needPasswordReset { get; set; }

    }
}
