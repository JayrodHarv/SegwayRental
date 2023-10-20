using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes {
    public class EmployeeAccessorFake : IEmployeeAccessor {
        // create a few fake employees for testing
        private List<EmployeeVM> fakeEmployees = new List<EmployeeVM>();
        private List<string> passwordHashes = new List<string>();

        public EmployeeAccessorFake() {
            fakeEmployees.Add(new EmployeeVM() {
                EmployeeID = 1,
                GivenName = "Tess",
                FamilyName = "Data",
                Phone = "1234567890",
                Email = "tess@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeEmployees.Add(new EmployeeVM() {
                EmployeeID = 2,
                GivenName = "Bess",
                FamilyName = "Data",
                Phone = "1234567890",
                Email = "bess@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeEmployees.Add(new EmployeeVM() {
                EmployeeID = 3,
                GivenName = "Jess",
                FamilyName = "Data",
                Phone = "1234567890",
                Email = "jess@company.com",
                Active = true,
                Roles = new List<string>()
            });

            passwordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            passwordHashes.Add("badhash");
            passwordHashes.Add("badhash");

            fakeEmployees[0].Roles.Add("TestRole1");
            fakeEmployees[0].Roles.Add("TestRole2");
        }
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash) {
            int numAuthenticated = 0;

            // check for employee records in the fake data
            for (int i = 0; i < fakeEmployees.Count; i++) {
                if (passwordHashes[i] == passwordHash &&
                    fakeEmployees[i].Email == email) {
                    numAuthenticated++;
                }
            }
            return numAuthenticated;
        }

        public EmployeeVM SelectEmployeeVMByEmail(string email) {
            EmployeeVM employeeVM = null;

            foreach (var fakeEmployee in fakeEmployees) {
                if(fakeEmployee.Email == email) {
                    employeeVM = fakeEmployee;
                }
            }
            if(employeeVM == null) {
                throw new ApplicationException("Employee not found");
            }

            return employeeVM;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID) {
            List<string> roles = new List<string>();

            foreach (var fakeEmployee in fakeEmployees) {
                if(fakeEmployee.EmployeeID == employeeID) {
                    roles = fakeEmployee.Roles;
                    break;
                }
            }
            return roles;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash) {
            int rows = 0;

            for (int i = 0; i < fakeEmployees.Count; i++) {
                if (fakeEmployees[i].Email == email) {
                    if (passwordHashes[i] == oldPasswordHash) {
                        passwordHashes[i] = newPasswordHash;
                        rows += 1;
                        
                    }
                }
            }
            if (rows != 1) {
                throw new ApplicationException("Bad email or password");
            }

            return rows;
        }
    }
}
