using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer {
    public interface IEmployeeManager {
        EmployeeVM LoginEmployee(string email, string password);

        // Helper Methods, public for reuse
        string HashSha256(string source);
        bool AuthenticateEmployee(string email, string password);
        EmployeeVM GetEmployeeVMByEmail(string email);
        List<string> GetRolesByEmployeeID(int employeeID);
        bool ResetPassword(string email, string oldPassword, string newPassword);

    }
}
