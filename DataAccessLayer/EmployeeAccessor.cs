using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer {
    public class EmployeeAccessor : IEmployeeAccessor {
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_authenticate_employee";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try {
                // open connection
                conn.Open();

                // execute command
                rows = Convert.ToInt32(cmd.ExecuteScalar());
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }
            return rows;
        }

        public EmployeeVM SelectEmployeeVMByEmail(string email) {
            EmployeeVM employeeVM = new EmployeeVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_employee_by_email";

            // create command
            var cmd = new SqlCommand(cmdText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@Email"].Value = email;

            try {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process results
                if (reader.HasRows) {
                    if (reader.Read()) { // use while loop if multiple rows are returned
                        employeeVM.EmployeeID = reader.GetInt32(0);
                        employeeVM.GivenName = reader.GetString(1);
                        employeeVM.FamilyName = reader.GetString(2);
                        employeeVM.Phone = reader.GetString(3);
                        employeeVM.Email = reader.GetString(4);
                        employeeVM.Active = reader.GetBoolean(5);
                        // null example
                        //employeeVM.EmployeeID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    }
                } else {
                    throw new ArgumentException("Employee not found");
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return employeeVM;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID) {
            List<string> roles = new List<string>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_roles_by_employeeID";

            // create command
            var cmd = new SqlCommand(cmdText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);

            // set parameter values
            cmd.Parameters["@EmployeeID"].Value = employeeID;

            try {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if(reader.HasRows) {
                    while(reader.Read()) {
                        roles.Add(reader.GetString(0));
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return roles;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash) {
            int rows = 0;

            // create connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_PasswordHash";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;

            try {

                conn.Open();

                // an update is executed nonquery (returns an int)
                rows = cmd.ExecuteNonQuery();

                if(rows == 0) {
                    // treat failed update as exception
                    throw new ArgumentException("Bad email or password");
                }

            } catch (Exception ex) {
                throw ex;
            } finally {
                conn.Close();
            }

            return rows;
        }
    }
}
