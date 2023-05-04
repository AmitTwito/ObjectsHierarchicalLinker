using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.BE
{
    public class ObjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ObjectModel> Children { get; set; }
        public ObjectModel()
        {
            this.Children = new List<ObjectModel>();
        }


        public void AddChild(ObjectModel item)
        {
            this.Children.Add(item);
        }

        public void FromJson(string json)
        {


        }
        public void ToJson(string json)
        {

        }

    }
}
