using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.Services
{
    public abstract class BaseService
    {
        protected readonly AuthenticationHeaderValue AuthToken;
        public BaseService()
        {
            var token = Preferences.Get("AuthToken",string.Empty);
            AuthToken = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
