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
        List<ObjectEntity> GetAll();
        void AddObjectEntity(ObjectEntity entity);
        List<ObjectEntity> GetChildrenByParentId(int parentId);
        void SaveAll(List<ObjectEntity> entities);
    }
}
