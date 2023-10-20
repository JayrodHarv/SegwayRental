using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces {
    public interface IEmployeeAccessor {
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        EmployeeVM SelectEmployeeVMByEmail(string email);
        List<string> SelectRolesByEmployeeID(int employeeID);
        int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);

    }
}
