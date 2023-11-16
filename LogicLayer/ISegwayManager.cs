using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface ISegwayManager {
        List<Segway> GetSegwaysByStatusID(string statusID);
        bool AddSegway(Segway segway);
        bool EditSegway(Segway oldSegway, Segway newSegway);
    }
}
