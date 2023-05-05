using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.BE
{
    public class ObjectsHierarchy : IJsonObjectConvertable
    {
        public List<ObjectEntity> _parents = new();
        public List<ObjectEntity> Parents
        {
            get
            {
                return new(_parents);
            }
        }

        public void Add(ObjectEntity objectEntity)
        {
            this._parents.Add(objectEntity);
        }

        // Tried to implement casting operator
        public object ToJsonObject()
        {
            var parents = new object[this._parents.Count];

            for (int i = 0; i < _parents.Count; i++)
                parents[i] = this._parents[i].ToJsonObject();
            return parents;
        }
    }
}
