using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchicalLinker.BE
{
    public enum ObjectsHierarchicalLinkerExpectionType
    {
        DALException,
        BadRequestException,
        InputValidationException,
        ServerError
    }

    public class ObjectsHierarchicalLinkerExpection : Exception
    {
        private ObjectsHierarchicalLinkerExpectionType _exceptionType;
        public ObjectsHierarchicalLinkerExpection(ObjectsHierarchicalLinkerExpectionType exceptionType, string message)
            : base(message)
        {
            _exceptionType = exceptionType;
        }

        public ObjectsHierarchicalLinkerExpectionType ExpcetionType() { return _exceptionType; }

    }

}
