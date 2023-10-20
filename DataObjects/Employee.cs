using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class Employee {
        /* Storage model for Employee table */
        public int EmployeeID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

    }

    public class EmployeeVM : Employee { 
        public List<string> Roles { get; set; }
    }
}
