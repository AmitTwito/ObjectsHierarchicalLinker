using Microsoft.Extensions.Logging;
using ObjectsHierarchyCreator.BE;
using ObjectsHierarchyCreator.DAL;
using System.Collections.Generic;
using System.Linq;

namespace ObjectsHierarchyCreator.BL
{

    public class ObjectEntitiesService : IObjectEntitiesService
    {
        private readonly IObjectEntitiesRepository _objectEntitiesRepo;
        private readonly ILogger<ObjectEntitiesService> _logger;
        public ObjectEntitiesService(IObjectEntitiesRepository objectEntitiesRepo, ILogger<ObjectEntitiesService> logger)
        {
            _objectEntitiesRepo = objectEntitiesRepo;
            _logger = logger;
        }

        public ObjectsHierarchy CreateAndGetHierarchy()
        {


            _logger.LogInformation("Creating and returning the hierarchy...");
            var objectEntities = this._objectEntitiesRepo.GetAll();

            var idToHierarchyObject = new Dictionary<int, HierarchyObject>();
            objectEntities.ForEach(entity => { idToHierarchyObject.Add(entity.Id, entity.AsHierarchyObject()); });

            var objectHierarchy = new ObjectsHierarchy();

            foreach (var objectEntity in objectEntities)
            {
                var hierarchyObject = idToHierarchyObject[objectEntity.Id];
                var children = this._objectEntitiesRepo.GetChildrenByParentId(objectEntity.Id);
                foreach (var child in children)
                    hierarchyObject.AddChild(idToHierarchyObject[child.Id]);
                if (objectEntity.ParentId == ObjectEntity.NoParentIdValue)
                    objectHierarchy.AddObject(hierarchyObject);
            }

            _logger.LogInformation("Done creating the hierarchy.");
            return objectHierarchy;
        }

        public List<ObjectEntity> GetAllObjects()
        {
            _logger.LogInformation("Getting all objects from DAL..");

            return this._objectEntitiesRepo.GetAll();
        }

        public List<ObjectEntity> ValidateInput(List<ObjectEntity> objectEntities)
        {
            _logger.LogInformation("Validating input...");

            var allObjectIdsSet = new HashSet<int>(objectEntities.Select(e => e.Id));
            HashSet<int> seenIds = new HashSet<int>();

            foreach (var objectEntity in objectEntities)
            {
                // Detect duplicate objects id's in the input and throw an exception.
                if (seenIds.Contains(objectEntity.Id))
                    throw new InvalidInputException($"There are more than one object with id {objectEntity.Id}");
                seenIds.Add(objectEntity.Id);

                var parentId = (int)objectEntity.ParentId;

                // Check if there is and object with a parent that does not exist.
                if (parentId != ObjectEntity.NoParentIdValue && !allObjectIdsSet.Contains(parentId))
                {
                    throw new InvalidInputException($"Object with id {objectEntity.Id} has a parent with Id {parentId} that does not exist.");
                }
            }
            _logger.LogInformation("Input is valid.");

            return objectEntities;
        }

        public void SaveObjectEntities(List<ObjectEntity> objectEntities)
        {
            _logger.LogInformation("Saving all object entities...");
            this._objectEntitiesRepo.SaveAll(objectEntities);
            _logger.LogInformation("Successfully saved all object entities.");

        }
    }
}
