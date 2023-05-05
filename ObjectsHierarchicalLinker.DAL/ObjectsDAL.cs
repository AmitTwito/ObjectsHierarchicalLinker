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
        private List<ObjectEntity> _objectEntities = new();
        private Dictionary<int, List<ObjectEntity>> _parentsIndex = new();

        public List<ObjectEntity> GetAll()
        {
            return this._objectEntities;
        }

        public List<ObjectEntity> GetChildrenByParentId(int parentId)
        {   
            if (this._parentsIndex.ContainsKey(parentId))
                return this._parentsIndex[parentId];
            else
                return new List<ObjectEntity>();
        }

        public void AddObjectEntity(ObjectEntity entity)
        {
            this._objectEntities.Add(entity);
            addEntityToDict(entity);
        }

        public void SaveAll(List<ObjectEntity> entities)
        {
            clearData();
            foreach (ObjectEntity entity in entities)
            {
                AddObjectEntity(entity);
            }
        }

        private void clearData()
        {
            this._objectEntities.Clear();
            this._parentsIndex.Clear();
        }


        private void addEntityToDict(ObjectEntity entity)
        {
            var parentId = (int)entity.Parent;
            if (parentId == -1)
                return;
            if (!this._parentsIndex.ContainsKey(parentId))
                this._parentsIndex[parentId] = new List<ObjectEntity>();
            
            this._parentsIndex[parentId].Add(entity);
        }
    }
}
