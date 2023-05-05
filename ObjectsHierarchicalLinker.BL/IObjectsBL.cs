using ObjectsHierarchicalLinker.BE;
using System.Collections.Generic;

namespace ObjectsHierarchicalLinker.BL
{
    public interface IObjectsBL
    {
        List<ObjectEntity> GetAllObjects();
        List<ObjectEntity> ParseInputAndGetObjectEntities(ObjectEntity[] objectModels);
        void SaveObjectEntities(List<ObjectEntity> objectEntities);
        ObjectsHierarchy CreateAndGetHeirarchy();
    }
}
