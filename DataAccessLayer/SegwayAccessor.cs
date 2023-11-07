using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer {
    public class SegwayAccessor : ISegwayAccessor {
        public List<Segway> SelectSegwaysByStatusID(string statusID) {
            List<Segway> segways = new List<Segway>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_segways_by_statusid";

            // create command
            var cmd = new SqlCommand(cmdText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@StatusID", SqlDbType.NVarChar, 25);

            // set parameter values
            cmd.Parameters["@StatusID"].Value = statusID;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process results
                if (reader.HasRows) {
                    while (reader.Read()) {
                        segways.Add(new Segway() {
                            SegwayID = reader.GetString(0),
                            Color = reader.GetString(1),
                            Name = reader.GetString(2),
                            TypeID = reader.GetString(3),
                            StatusID = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                        });
                    }
                } else {
                    throw new ArgumentException("Segways not found");
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return segways;
        }
    }
}
