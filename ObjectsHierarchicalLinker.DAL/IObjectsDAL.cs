using ObjectsHierarchicalLinker.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.DAL
{

    public interface IObjectsDAL
    {
        List<ObjectModel> GetAllObjects();
        void SaveObjects(ObjectModel[] objectModels);
        Dictionary<int,List<ObjectModel>> GetParentsChildrenListDict();
    }
}
