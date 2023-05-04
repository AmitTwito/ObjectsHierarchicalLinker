using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.BE
{
    public class ObjectEntity : IJsonConvertable
    {
        public int Id { get; }
        public string Name { get; }
        public int Parent { get; }
        private List<ObjectEntity> _children;

        public ObjectEntity()
        {
            this._children = new List<ObjectEntity>();
        }

        public void AddChild(ObjectEntity item)
        {
            this._children.Add(item);
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }

        public void FromJson(string json)
        {
            throw new NotImplementedException();
        }
    }
}
