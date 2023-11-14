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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;
using DataAccessFakes;

namespace WPFPresentation {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        EmployeeManager _employeeManager = null;
        EmployeeVM loggedInEmployee = null; // no one is logged in at first

        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            // test code
            // _employeeManager = new EmployeeManager(new EmployeeAccessorFake());

            // real code with live database
            _employeeManager = new EmployeeManager();
            txtEmail.Focus();
            hideAllTabs();
            btnLogin.IsDefault = true;
        }

        private void hideAllTabs() {
            foreach (var tab in tabsetMain.Items) {
                ((TabItem)tab).Visibility = Visibility.Collapsed;
            }
            tabsetMain.Visibility = Visibility.Collapsed;
            tabContainer.Visibility = Visibility.Collapsed;
        }

        private void showTabsForRoles() {
            // loop through user roles
            foreach (var role in loggedInEmployee.Roles) {
                switch(role) {
                    case "Rental":
                        tabRental.Visibility = Visibility.Visible;
                        break;
                    case "TourGuide":
                        tabTour.Visibility = Visibility.Visible;
                        break;
                    case "CheckIn":
                        tabCheckIn.Visibility = Visibility.Visible;
                        break;
                    case "Maintenance":
                        tabMaintenance.Visibility = Visibility.Visible;
                        break;
                    case "Prep":
                        tabPrep.Visibility = Visibility.Visible;
                        break;
                    case "Manager":
                        tabManager.Visibility = Visibility.Visible;
                        tabRental.Visibility = Visibility.Visible;
                        tabTour.Visibility = Visibility.Visible;
                        tabCheckIn.Visibility = Visibility.Visible;
                        tabMaintenance.Visibility = Visibility.Visible;
                        tabPrep.Visibility = Visibility.Visible;
                        break;
                    case "Admin":
                        tabAdmin.Visibility = Visibility.Visible;
                        break;
                }
            }
            tabsetMain.Visibility = Visibility.Visible;
            tabContainer.Visibility = Visibility.Visible;
        }

        private void updateUIForEmployee() {
            string rolesList = "";
            for (int i = 0; i < loggedInEmployee.Roles.Count; i++) {
                rolesList += " " + loggedInEmployee.Roles[i];
                if(i == loggedInEmployee.Roles.Count - 2) {
                    if(loggedInEmployee.Roles.Count > 2) {
                        rolesList += ",";
                    }
                    rolesList += " and";
                } else if (i < loggedInEmployee.Roles.Count - 2) {
                    rolesList += ",";
                }
            }
            lblGreeting.Content = "Welcome " + loggedInEmployee.GivenName + 
                ". Your are logged in as: " + rolesList + ".";
            statMessage.Content = "Logged in on " + DateTime.Now.ToLongDateString() +
                " at " + DateTime.Now.ToShortTimeString() +
                ". Please remember to log out before leaving your workstation.";
            // clear the login section
            txtEmail.Text = "";
            txtEmail.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            pwdPassword.Password = "";
            pwdPassword.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;

            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;

            showTabsForRoles();
        }

        private void updateUIForLogout() {
            lblGreeting.Content = "You are not currently logged in";
            statMessage.Content = "Welcome. Please log in to continue.";
            // clear the login section
            txtEmail.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;

            btnLogin.Content = "Log In";
            btnLogin.IsDefault = true;

            hideAllTabs();
            loggedInEmployee = null;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {
            // test code
            //string password = pwdPassword.Password;
            //string email = txtEmail.Text;

            //// put some error checks here
            //EmployeeVM employeeVM = _employeeManager.LoginEmployee(email, password);
            //if(employeeVM != null) {
            //    MessageBox.Show("Welcome " + employeeVM.GivenName + "\n" +
            //        "You are logged in as " + employeeVM.Roles[0] + ".");
            //} else {
            //    MessageBox.Show("Authentication failed");
            //}

            if(btnLogin.Content.ToString() == "Log In") {
                var email = txtEmail.Text;
                var password = pwdPassword.Password;

                // error checks
                if(!email.IsValidEmail()) { 
                    MessageBox.Show("That is not a valid email address", "Invalid Email Address",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    txtEmail.SelectAll();
                    txtEmail.Focus();
                    return;
                }

                if (!password.IsValidPassword()) {
                    MessageBox.Show("That is not a valid password", "Invalid Password",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    pwdPassword.SelectAll();
                    pwdPassword.Focus();
                    return;
                }

                // try to login the user
                try {
                    loggedInEmployee = _employeeManager.LoginEmployee(email, password);

                    // we need to check for first login for new user
                    if(pwdPassword.Password == "newuser") {
                        try {
                            var passwordWindow = new PasswordChangeWindow(loggedInEmployee.Email);
                            var result = passwordWindow.ShowDialog();
                            if (result == true) {
                                MessageBox.Show("Password changed.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            } else {
                                MessageBox.Show("Password not changed. \nYou must change your password to continue", "Logging Out",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                updateUIForLogout();
                                return;
                            }
                        } catch (Exception ex) {
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                                "Update failed", MessageBoxButton.OK, MessageBoxImage.Error);
                            updateUIForLogout();
                            return;
                        }
                    }

                    // update the UI if employee is logged in and if newuser has updated password.
                    updateUIForEmployee();

                } catch (Exception ex) {
                    // you may never throw exceptions from the presentation layer
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, 
                        "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    pwdPassword.Clear();
                    txtEmail.Clear();
                    txtEmail.Focus();
                    return;
                }

            } else { // log out
                updateUIForLogout();
            }
            

        }

        private void mnuUpdatePassword_Click(object sender, RoutedEventArgs e) {
            if(loggedInEmployee == null) {
                MessageBox.Show("You must be logged in to change your password",
                    "Login Required", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            } else {
                try {
                    var passwordWindow = new PasswordChangeWindow(loggedInEmployee.Email);
                    var result = passwordWindow.ShowDialog();
                    if (result == true) {
                        MessageBox.Show("Password changed.", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    } else {
                        MessageBox.Show("Password not changed", "Operation Aborted",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Update failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void tabRental_GotFocus(object sender, RoutedEventArgs e) {
            try {
                if (datRental.ItemsSource == null) {
                    var segwayManger = new SegwayManager();
                    datRental.ItemsSource = segwayManger.GetSegwaysByStatusID("For Rent");
                    datRental.Columns.RemoveAt(5);
                    datRental.Columns.RemoveAt(4);

                    datRental.Columns[0].Header = "Segway ID";
                    datRental.Columns[3].Header = "Type";
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void datRental_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var segway = datRental.SelectedItem as Segway;
            // MessageBox.Show("You chose " + segway.Color + " " + segway.Name);
            var addEditWindow = new SegwayAddEditDetail(segway);
            addEditWindow.ShowDialog();
        }
    }
}
