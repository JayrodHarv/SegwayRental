using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicLayer;

namespace WPFPresentation {
    /// <summary>
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window {

        private string _email;
        public PasswordChangeWindow(string email) {

            _email = email;

            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e) {

            EmployeeManager employeeManager = new EmployeeManager();

            string oldPassword = pwdOldPassword.Password;
            string newPassword = pwdNewPassword.Password;
            string retypePassword = pwdRetypePassword.Password;

            if(newPassword == "newuser") {
                MessageBox.Show("Nice try. Now really change your password.", "Busted",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            // error checks
            if (newPassword == oldPassword) {
                MessageBox.Show("You can't reuse the same password.", "Busted",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            if (newPassword != retypePassword) {
                MessageBox.Show("New password and retype password need to be the same", "Busted",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }


            try {
                if (employeeManager.ResetPassword(_email, pwdOldPassword.Password, pwdNewPassword.Password)) {
                    this.DialogResult = true;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                this.DialogResult = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            btnSubmit.IsDefault = true;
            pwdOldPassword.Focus();
        }
    }
}
