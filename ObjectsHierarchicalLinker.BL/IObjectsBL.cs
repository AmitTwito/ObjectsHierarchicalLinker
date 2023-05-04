using ObjectsHierarchicalLinker.BE;
using System.Collections.Generic;

namespace ObjectsHierarchicalLinker.BL
{
    public interface IObjectsBL
    {
        List<ObjectModel> SaveObjectsAndGetLinkedHeirarchy(ObjectModel[] objectModels);

        List<ObjectModel> GetAllObjects();

    }
}
