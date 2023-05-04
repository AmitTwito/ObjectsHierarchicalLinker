using ObjectsHierarchicalLinker.BE;
using System.Collections.Generic;

namespace ObjectsHierarchicalLinker.BL
{
    public interface IObjectsBL
    {
        List<ObjectEntity> SaveObjectsAndGetLinkedHeirarchy(ObjectEntity[] objectEntities);

        List<ObjectEntity> GetAllObjects();

    }
}
