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

        public List<string> GetAllStatuses() {
            List<string> statuses = new List<string>();

            try {
                statuses = _segwayAccessor.SelectAllStatuses();

                if (statuses.Count == 0) {
                    throw new ArgumentException("Statuses not found");
                }
            } catch (Exception ex) {
                throw ex;
            }

            return statuses;
        }

        public List<string> GetAllTypes() {
            List<string> types = new List<string>();

            try {
                types = _segwayAccessor.SelectAllTypes();

                if (types.Count == 0) {
                    throw new ArgumentException("Types not found");
                }
            } catch (Exception ex) {
                throw ex;
            }

            return types;
        }

        public bool AddSegway(Segway segway) {
            bool result = false;
            try {
                result = (1 == _segwayAccessor.InsertSegway(segway));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool EditSegway(Segway oldSegway, Segway newSegway) {
            bool result = false;
            try {
                result = (1 == _segwayAccessor.UpdateSegway(oldSegway, newSegway));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }
    }
}
