using ObjectsHierarchyCreator.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.DAL
{

    public interface IObjectEntitiesDAL
    {
        List<ObjectEntity> GetAll();
        void AddObjectEntity(ObjectEntity entity);
        List<ObjectEntity> GetChildrenByParentId(int parentId);
        void SaveAll(List<ObjectEntity> entities);
    }
}
