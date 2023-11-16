using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface ISegwayAccessor {
        List<Segway> SelectSegwaysByStatusID(string statusID);
        List<string> SelectAllStatuses();
        List<string> SelectAllTypes();
        int UpdateSegway(Segway oldSegway, Segway newSegway);
        int InsertSegway(Segway segway);
    }
}
