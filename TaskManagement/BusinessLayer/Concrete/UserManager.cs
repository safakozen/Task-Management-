using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserDAL _userdal;

        public UserManager(IUserDAL userdal)
        {
            _userdal = userdal;
        }
        public List<User> GetUserList()
        {
            return _userdal.List();
        }
        public User GetUserById(int id)
        {
            return _userdal.List(x => x.Id == id).SingleOrDefault();
        }
        public void CreateUser(User user)
        {
            _userdal.Insert(user);
        }
        public void DeleteUser(User user)
        {
            _userdal.Delete(user);
        }
        public void UpdateUser(User user)
        {
            _userdal.Update(user);
        }
        public User Login(User model)
        {
            User user = _userdal.Find(x=>x.Email== model.Email && x.Password== model.Password);
            if (user!=null)
            {
                return user;
            }
            return null;
        }
    }
}
