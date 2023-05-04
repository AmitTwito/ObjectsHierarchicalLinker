using ObjectsHierarchicalLinker.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.DAL
{
    public class ObjectsDAL : IObjectsDAL
    {
        private List<ObjectModel> _objectModels;
        private Dictionary<int, List<ObjectModel>> _parentsIndex;
        public ObjectsDAL()
        {
            this._objectModels = new List<ObjectModel>();
            this._parentsIndex = new Dictionary<int, List<ObjectModel>>();
        }

        public List<ObjectModel> GetAllObjects()
        {
            return this._objectModels;
        }

        public Dictionary<int, List<ObjectModel>> GetParentsChildrenListDict()
        {
            return new Dictionary<int, List<ObjectModel>>(_parentsIndex);
        }

        public void SaveObjects(ObjectModel[] objectModels)
        {
            this._objectModels = objectModels.ToList();
            var parentsIds = this._objectModels.Select(x=> x.Parent).ToList();
            foreach (var id in parentsIds)
            {
                this._parentsIndex[id] = new List<ObjectModel>();
            }

            foreach (ObjectModel objectModel in objectModels)
            {
                this._parentsIndex[objectModel.Parent].Add(objectModel);
            }
        }

        public void SaveNewObject(ObjectModel newObject)
        {
            this._objectModels.Add(newObject);
            if (this._parentsIndex.ContainsKey(newObject.Parent))
                this._parentsIndex[newObject.Parent].Add(newObject);
            else
                this._parentsIndex[newObject.Parent] = new List<ObjectModel>();
        }
    }
}
