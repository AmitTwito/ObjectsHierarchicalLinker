using ObjectsHierarchyCreator.BE.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.DAL
{
    public interface IAccessControlRepository
    {

        List<User> GetAllUsers();
        User GetUserByCredentials(AuthRequest authRequest);
    }
}
