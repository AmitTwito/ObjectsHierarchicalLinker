using ObjectsHierarchicalLinker.BE;
using ObjectsHierarchicalLinker.DAL;
using System.Collections.Generic;
using System.Linq;

namespace ObjectsHierarchicalLinker.BL
{

    public class ObjectsBL : IObjectsBL
    {
        private readonly IObjectsDAL _objectsDAL;
        public ObjectsBL(IObjectsDAL objectsDAL)
        {
            _objectsDAL = objectsDAL;
        }

        public List<ObjectEntity> GetAllObjects()
        {
            return this._objectsDAL.GetAll();
        }

        public List<ObjectEntity> SaveObjectsAndGetLinkedHeirarchy(ObjectEntity[] objectEntities)
        {
            this._objectsDAL.SaveAll(objectEntities.ToList());

            return getLinkedHeirarchyCollection();
        }

        private List<ObjectEntity> getLinkedHeirarchyCollection()
        {
            var objectEntities = this._objectsDAL.GetAll();
            var resultObjects = new List<ObjectEntity>();

            foreach (var objectEntity in objectEntities)
            {
                var objectId = objectEntity.Id;
                var children = this._objectsDAL.GetChildrenByParentId(objectId);
                foreach (var child in children)
                    objectEntity.AddChild(child);
                if (objectEntity.Parent == -1)
                    resultObjects.Add(objectEntity);
            }

            return resultObjects;
        }



    }
}
