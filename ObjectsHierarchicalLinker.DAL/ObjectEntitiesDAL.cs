using ObjectsHierarchyCreator.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.DAL
{
    public class ObjectEntitiesDAL : IObjectEntitiesDAL
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

        public void SaveAll(List<ObjectEntity> entities)
        {
            ClearData();
            foreach (ObjectEntity entity in entities)
            {
                AddObjectEntity(entity);
            }
        }

        public void AddObjectEntity(ObjectEntity entity)
        {
            this._objectEntities.Add(entity);
            AddEntityToIndexDict(entity);
        }

        private void ClearData()
        {
            this._objectEntities.Clear();
            this._parentsIndex.Clear();
        }

        private void AddEntityToIndexDict(ObjectEntity entity)
        {
            var parentId = (int)entity.ParentId;
            if (parentId == ObjectEntity.NoParentIdValue)
                return;
            if (!this._parentsIndex.ContainsKey(parentId))
                this._parentsIndex[parentId] = new List<ObjectEntity>();

            this._parentsIndex[parentId].Add(entity);
        }
    }
}
