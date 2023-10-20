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

            try {
                if (employeeManager.ResetPassword(_email, pwdOldPassword.Password, pwdNewPassword.Password)) {
                    this.DialogResult = true;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                this.DialogResult = false;
            }
        }
    }
}
