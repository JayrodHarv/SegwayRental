using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessInterfaces;

namespace LogicLayer {
    public class SegwayManager : ISegwayManager {
        private ISegwayAccessor _segwayAccessor = null;

        public SegwayManager() {
            _segwayAccessor = new SegwayAccessor();
        }

        public SegwayManager(ISegwayAccessor segwayAccessor) {
            _segwayAccessor = segwayAccessor;
        }
        

        public List<Segway> GetSegwaysByStatusID(string statusID) {
            List<Segway> segways = null;

            try {
                segways = _segwayAccessor.SelectSegwaysByStatusID(statusID);

                if(segways == null || segways.Count == 0) {
                    throw new ArgumentException("Segways not found");
                }
            } catch (Exception ex) {
                throw new ApplicationException("Not found", ex);
            }

            return segways;
        }
    }
}
