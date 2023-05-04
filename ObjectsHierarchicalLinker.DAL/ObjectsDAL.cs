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
        private List<ObjectEntity> _objectEntities;
        private Dictionary<int, List<ObjectEntity>> _parentsIndex;
        public ObjectsDAL()
        {
            this._objectEntities = new List<ObjectEntity>();
            this._parentsIndex = new Dictionary<int, List<ObjectEntity>>();
        }

        public List<ObjectEntity> GetAll()
        {
            return this._objectEntities;
        }

        public List<ObjectEntity> GetChildrenByParentId(int parentId)
        {
            return this._parentsIndex[parentId];
        }

        public void AddObjectEntity(ObjectEntity entity)
        {
            this._objectEntities.Add(entity);
            addEntityToDict(entity);
        }

        public void SaveAll(List<ObjectEntity> entities)
        {
            clearData();
            entities.ToList().ForEach(e => this.AddObjectEntity(e));
        }

        private void clearData()
        {
            this._objectEntities.Clear();
            this._parentsIndex.Clear();
        }


        private void addEntityToDict(ObjectEntity entity)
        {
            var parentId = entity.Parent;
            if (parentId == -1)
                return;
            if (!this._parentsIndex.ContainsKey(entity.Parent))
                this._parentsIndex[entity.Parent] = new List<ObjectEntity>();
            else
                this._parentsIndex[entity.Parent].Add(entity);
        }
    }
}
