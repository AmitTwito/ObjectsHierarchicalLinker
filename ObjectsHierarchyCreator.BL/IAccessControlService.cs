using ObjectsHierarchyCreator.BE.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.BL
{
    public interface IAccessControlService
    {

        List<User> GetAllUsers();
        Token GetAccessToken(AuthRequest request);
        
    }
}
