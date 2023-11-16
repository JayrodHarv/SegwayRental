using DataObjects;
using LogicLayer;
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

namespace WPFPresentation {
    /// <summary>
    /// Interaction logic for SegwayAddEditDetail.xaml
    /// </summary>
    public partial class SegwayAddEditDetail : Window {
        private Segway segway = null;
        public SegwayAddEditDetail(Segway s) {
            segway = s;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                var segwayManager = new SegwayManager();
                cboStatus.ItemsSource = segwayManager.GetAllStatuses();
                cboType.ItemsSource = segwayManager.GetAllTypes();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

            txtSegwayID.Text = segway.SegwayID;
            txtName.Text = segway.Name;
            txtColor.Text = segway.Color;
            cboStatus.Text = segway.StatusID;
            cboType.Text = segway.TypeID;
            chkActive.IsChecked = segway.Active;

            txtSegwayID.IsReadOnly = true;
            txtName.IsReadOnly = true;
            cboType.IsReadOnly = true;
            cboStatus.IsReadOnly = true;
            chkActive.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            if(btnAdd.Content.ToString() == "Add") {
                btnEdit.IsEnabled = false;
                txtSegwayID.Text = "";
                txtName.Text = "";
                txtColor.Text = "";
                cboStatus.Text = "";
                cboType.Text = "";
                chkActive.IsChecked = true;

                txtSegwayID.IsReadOnly = false;
                txtName.IsReadOnly = false;
                txtColor.IsReadOnly = false;
                cboStatus.IsReadOnly = false;
                cboType.IsReadOnly = false;

                btnAdd.Content = "Save";
            } else if(btnAdd.Content.ToString() == "Save") {
                if(txtSegwayID.Text == "" || txtName.Text == "" || txtColor.Text == "" || cboStatus.Text == "" || cboType.Text == "" ) {
                    MessageBox.Show("Please fill out all fields");
                    txtSegwayID.Focus();
                    return;
                }

                var newSegway = new Segway() {
                    SegwayID = txtSegwayID.Text,
                    Name = txtName.Text,
                    Color = txtColor.Text,
                    TypeID = cboType.Text,
                    StatusID = cboStatus.Text
                };

                try {
                    var sm = new SegwayManager();
                    bool result = sm.AddSegway(newSegway);
                    this.Close();
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {

        }
    }
}
