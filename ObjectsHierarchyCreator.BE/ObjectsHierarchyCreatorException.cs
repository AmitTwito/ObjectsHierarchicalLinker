using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.BE
{


    public class ObjectsHierarchyCreatorException : Exception
    {
        public ObjectsHierarchyCreatorException(string message)
            : base(message)
        {
        }
    }

    public class InvalidInputException : ObjectsHierarchyCreatorException
    {
        public InvalidInputException(string message)
            : base($"Invalid input: {message}")
        {
        }
    }

    public class DALException : ObjectsHierarchyCreatorException
    {
        public DALException(string message)
            : base($"Data access layer error: {message}")
        {
        }
    }

}
