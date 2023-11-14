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
    }
}
