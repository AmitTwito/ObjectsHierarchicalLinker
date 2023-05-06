using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.BE
{
    public class HierarchyObject
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("childs")]
        public List<HierarchyObject> Children { get; set; }

        public HierarchyObject()
        {
            Children = new List<HierarchyObject>();
        }
        public void AddChild(HierarchyObject child)
        {
            Children.Add(child);
        }

       
    }
}
