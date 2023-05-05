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

        public ObjectsHierarchy CreateAndGetHeirarchy()
        {
            var objectEntities = this._objectsDAL.GetAll();
            var objectHierarchy = new ObjectsHierarchy();

            foreach (var objectEntity in objectEntities)
            {
                var children = this._objectsDAL.GetChildrenByParentId(objectEntity.Id);
                foreach (var child in children)
                    objectEntity.AddChild(child);
                if (objectEntity.Parent == -1)
                    objectHierarchy.Add(objectEntity);
            }

            return objectHierarchy;
        }

        public List<ObjectEntity> GetAllObjects()
        {
            return this._objectsDAL.GetAll();
        }

        public List<ObjectEntity> ParseInputAndGetObjectEntities(ObjectEntity[] objectModels)
        {
            var entities = this._objectsDAL.GetAll();
            return entities;
        }

        public void SaveObjectEntities(List<ObjectEntity> objectEntities)
        {
            this._objectsDAL.SaveAll(objectEntities);
        }
    }
}
