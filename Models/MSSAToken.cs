using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.Models
{
    public class MSSAToken
    {
        public string token {  get; set; }
        public string expires {  get; set; }
    }

    public class ActiveToken
    {
        public SecurityToken Token { get; set; }
        public DateTimeOffset Expiration { get; set; }

        public ActiveToken(SecurityToken token, DateTimeOffset expiration)
        {
            Token = token;
            Expiration = expiration;
        }
        
    }

    public class ActiveUser
    {
        public string UserID { get; set; }
        public ActiveToken UserToken { get; set; }

        public ActiveUser(string userID, ActiveToken userToken)
        {
            UserID = userID;
            UserToken = userToken;
        }

        public ActiveUser() 
        {
            UserID = "default";
            UserToken = null;
        }
    }
}
