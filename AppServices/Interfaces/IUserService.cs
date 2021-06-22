using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SocialSecurityNumberWebApp.Data.Models;

namespace AppServices.Interfaces
{
    public interface IUserService
    {
        void Add(User user);

        void Update(User user);

        User Get(Guid id);

        List<User> GetAll();

        void Delete(Guid id);
    }
}
