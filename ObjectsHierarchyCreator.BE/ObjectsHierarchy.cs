using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace ObjectsHierarchyCreator.BE
{
/*
 Represents the hierarchy of the objects.
 */
    public class ObjectsHierarchy
    {
        public List<HierarchyObject> Objects { get; }

        public ObjectsHierarchy()
        {
            Objects = new List<HierarchyObject>();
        }

        public void AddObject(HierarchyObject hierarchyObject)
        {
            this.Objects.Add(hierarchyObject);
        }
        public string ToJsonString()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            return JsonSerializer.Serialize(Objects, options);
        }

    }
}
