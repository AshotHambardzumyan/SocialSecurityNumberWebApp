using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Interfaces;
using System.Collections.Generic;
using SocialSecurityNumberWebApp.Data.Data;
using SocialSecurityNumberWebApp.Data.Models;

namespace AppServices.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserDbContext _dbContext;
        public UserService(IUserDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Add(User user)
        {
            _dbContext.Users.Add(user);
        }

        public void Delete(Guid id)
        {
            _dbContext.Users.Remove(Get(id));
        }

        public User Get(Guid id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _dbContext.Users;
        }

        public void Update(User user)
        {
            var oldUser = Get(user.Id);

            int index = _dbContext.Users.IndexOf(oldUser);

            _dbContext.Users[index] = user;
        }
    }
}