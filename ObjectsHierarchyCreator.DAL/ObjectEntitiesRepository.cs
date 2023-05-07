using ObjectsHierarchyCreator.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.DAL
{
    public class ObjectEntitiesRepository : IObjectEntitiesRepository
    {
        private List<ObjectEntity> _objectEntities = new();
        private Dictionary<int, List<ObjectEntity>> _parentsIndex = new();

        public List<ObjectEntity> GetAll()
        {
            return this._objectEntities;
        }

        public List<ObjectEntity> GetChildrenByParentId(int parentId)
        {
            try
            {
                if (this._parentsIndex.ContainsKey(parentId))
                    return this._parentsIndex[parentId];
                else
                    return new List<ObjectEntity>();
            }
            catch (Exception ex)
            {
                throw new DALException($"Error at GetChildrenByParentId - {ex.Message}");
            }
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
            try
            {
                this._objectEntities.Add(entity);
                AddEntityToIndexDict(entity);
            }
            catch (Exception ex)
            {
                throw new DALException($"Error at AddObjectEntity. Object ID: {entity.Id} - {ex.Message}");
            }
        }

        private void ClearData()
        {
            try 
            {
                this._objectEntities.Clear();
                this._parentsIndex.Clear();
            }
            catch (Exception ex)
            {
                throw new DALException($"Error at ClearData - {ex.Message}");
            }
            
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
