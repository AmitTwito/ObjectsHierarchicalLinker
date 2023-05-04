using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.BE
{
    public interface IJsonConvertable
    { 
    
        void FromJson(string json);
        string ToJson();
    }
}
