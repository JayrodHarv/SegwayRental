using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {

    // This class will hold extension helpers
    public static class ValidationHelpers {
        // extension methods must be public, static, and include 'this'
        // as the first parameter, with the type being extended following
        public static bool IsValidEmail(this string email) {
            bool isValid = false;

            if(email.EndsWith("@dundermifflin.com") &&
               email.Length > 13 &&
               email.Length <= 100) {
                isValid = true;
            }

            return isValid;
        }

        public static bool IsValidPassword(this string password) {
            bool isValid = false;

            // this needs to be done right eventually
            if(password.Length >= 7) {
                isValid = true;
            }

            return isValid;
        }
    }
}
