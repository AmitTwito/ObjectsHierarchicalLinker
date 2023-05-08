using ObjectsHierarchyCreator.BE;
using ObjectsHierarchyCreator.BE.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.DAL
{
    public class AccessControlRepository : IAccessControlRepository
    {
        private List<User> _users = new() { new User() { Username = "admin", Password = "123" } };


        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserByCredentials(AuthRequest authRequest)
        {
            try
            {
                return _users.Where(u => u.Username == authRequest.Username && u.Password == authRequest.Password).FirstOrDefault();

            }
            catch (Exception e)
            {
                throw new DALException($"Error at GetUserByCredentials. '{e.Message}'");
            }
        }
    }
}