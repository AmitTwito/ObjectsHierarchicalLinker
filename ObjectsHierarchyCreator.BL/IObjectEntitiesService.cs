using ObjectsHierarchyCreator.BE;
using System.Collections.Generic;

namespace ObjectsHierarchyCreator.BL
{
    public interface IObjectEntitiesService
    {
        List<ObjectEntity> GetAllObjects();
        List<ObjectEntity> ValidateInput(List<ObjectEntity> objectEntities);
        void SaveObjectEntities(List<ObjectEntity> objectEntities);
        ObjectsHierarchy CreateAndGetHierarchy();
    }
}
