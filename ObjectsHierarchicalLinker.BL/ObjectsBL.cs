using ObjectsHierarchicalLinker.BE;
using ObjectsHierarchicalLinker.DAL;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace ObjectsHierarchicalLinker.BL
{

    public class ObjectsBL : IObjectsBL
    {
        private readonly IObjectsDAL _objectsDAL;
        public ObjectsBL(IObjectsDAL objectsDAL)
        {
            _objectsDAL = objectsDAL;
        }

        public List<ObjectModel> GetAllObjects()
        {
            return this._objectsDAL.GetAllObjects();
        }

        public List<ObjectModel> SaveObjectsAndGetLinkedHeirarchy(ObjectModel[] objectModels)
        {
            this._objectsDAL.SaveObjects(objectModels);

            return getLinkedHeirarchy();
        }


        private List<ObjectModel> getLinkedHeirarchy()
        {
            var objects = this._objectsDAL.GetAllObjects();
            var parentsToChildrenDict = this._objectsDAL.GetParentsChildrenListDict();
            var resultObjects = new List<ObjectModel>();

            foreach (var objectModel in objects)
            {

                

                if (objectModel.Parent == -1) 
                    resultObjects.Add(objectModel);

            }

            return resultObjects;
        }



    }
}
