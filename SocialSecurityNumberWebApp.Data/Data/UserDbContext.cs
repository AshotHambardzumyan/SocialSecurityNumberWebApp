using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SocialSecurityNumberWebApp.Data.Models;

namespace SocialSecurityNumberWebApp.Data.Data
{
    public class UserDbContext : IUserDbContext
    {
        public List<User> Users { get; set; }

        public UserDbContext()
        {
            Users = new List<User>();
        }
    }

    public interface IUserDbContext
    {
        List<User> Users { get; set; }
    }
}