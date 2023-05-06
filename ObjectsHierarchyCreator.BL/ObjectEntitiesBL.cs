using ObjectsHierarchyCreator.BE;
using ObjectsHierarchyCreator.DAL;
using System.Collections.Generic;
using System.Linq;

namespace ObjectsHierarchyCreator.BL
{

    public class ObjectEntitiesBL : IObjectEntitiesBL
    {
        private readonly IObjectEntitiesDAL _objectEntitiesDAL;
        public ObjectEntitiesBL(IObjectEntitiesDAL objectsDAL)
        {
            _objectEntitiesDAL = objectsDAL;
        }

        public ObjectsHierarchy CreateAndGetHierarchy()
        {
            var objectEntities = this._objectEntitiesDAL.GetAll();

            var idToHierarchyObject = new Dictionary<int, HierarchyObject>();
            objectEntities.ForEach(entity => { idToHierarchyObject.Add(entity.Id, entity.AsHierarchyObject()); });

            var objectHierarchy = new ObjectsHierarchy();

            foreach (var objectEntity in objectEntities)
            {
                var hierarchyObject = idToHierarchyObject[objectEntity.Id];
                var children = this._objectEntitiesDAL.GetChildrenByParentId(objectEntity.Id);
                foreach (var child in children)
                    hierarchyObject.AddChild(idToHierarchyObject[child.Id]);
                if (objectEntity.ParentId == ObjectEntity.NoParentIdValue)
                    objectHierarchy.AddObject(hierarchyObject);
            }

            return objectHierarchy;
        }

        public List<ObjectEntity> GetAllObjects()
        {
            return this._objectEntitiesDAL.GetAll();
        }

        public List<ObjectEntity> ValidateInput(List<ObjectEntity> objectEntities)
        {
            var allIds = new HashSet<int>(objectEntities.Select(e => e.Id));
            HashSet<int> seenIds = new HashSet<int>();
            var missingParents = new List<ObjectEntity>();

            foreach (var objectEntity in objectEntities)
            {
                if (seenIds.Contains(objectEntity.Id))
                    throw new InvalidInputException($"There are more than one object with id {objectEntity.Id}");
                seenIds.Add(objectEntity.Id);

                var parentId = (int)objectEntity.ParentId;
                if (parentId != ObjectEntity.NoParentIdValue && !allIds.Contains(parentId))
                {
                    allIds.Add(parentId);
                    missingParents.Add(new ObjectEntity() { ParentId = ObjectEntity.NoParentIdValue, Name = "Missing Parent Name", Id = parentId });
                }
            }

            return objectEntities.Concat(missingParents).ToList();
        }

        public void SaveObjectEntities(List<ObjectEntity> objectEntities)
        {
            this._objectEntitiesDAL.SaveAll(objectEntities);
        }
    }
}
