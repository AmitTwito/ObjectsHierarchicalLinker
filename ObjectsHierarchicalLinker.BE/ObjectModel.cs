using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.BE
{
    public class ObjectModel : IJsonConvertable
    {
        public int Id { get; }
        public string Name { get; }
        public int Parent { get; }
        private List<ObjectModel> _children;

        public ObjectModel()
        {
            this._children = new List<ObjectModel>();
        }

        public void AddChild(ObjectModel item)
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
