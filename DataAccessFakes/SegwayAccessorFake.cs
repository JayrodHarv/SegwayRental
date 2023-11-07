using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes {
    public class SegwayAccessorFake : ISegwayAccessor {
        private List<Segway> _segways = new List<Segway>();

        public SegwayAccessorFake() {
            _segways.Add(new Segway {
                SegwayID = "XXXX",
                TypeID = "Extra",
                StatusID = "For Rent",
                Color = "Green",
                Name = "XName",
                Active = true
            });
            _segways.Add(new Segway {
                SegwayID = "YYYY",
                TypeID = "Large",
                StatusID = "For Rent",
                Color = "Blue",
                Name = "YName",
                Active = true
            });
            _segways.Add(new Segway {
                SegwayID = "ZZZZ",
                TypeID = "Small",
                StatusID = "Needs Maintenance",
                Color = "Yellow",
                Name = "ZName",
                Active = true
            });
        }

        public List<Segway> SelectSegwaysByStatusID(string statusID) {
            return _segways.FindAll(s => s.StatusID == statusID); // Lambda for the win
        }
    }
}
