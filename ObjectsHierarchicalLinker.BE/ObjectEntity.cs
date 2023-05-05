using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.BE
{
    public class ObjectEntity : IJsonObjectConvertable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private int _parent;
        public int? Parent
        {
            get
            {
                return _parent;
            }
            set { _parent = value == null ? -1 : value.Value; }
        }

        public List<ObjectEntity> Childs { get; }

        public ObjectEntity()
        {
            Childs = new List<ObjectEntity>();
        }
        public void AddChild(ObjectEntity child)
        {
            if (child.Parent == Id)
                this.Childs.Add(child);
            //else
        }

        // Tried to implement casting
        public object ToJsonObject()
        {

            var childObjects = new object[Childs.Count];
            for (int i = 0; i < Childs.Count; i++)
                childObjects[i] = Childs[i].ToJsonObject();
            return new { id = Id, name = Name, childs = childObjects.ToArray() };
        }

    }
}
