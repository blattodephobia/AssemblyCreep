using AssemblyCreep.ViewModels;
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

namespace AssemblyCreep.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TargetProjectView : UserControl
    {
        public MergerViewModel ViewModel
        {
            get
            {
                return DataContext as MergerViewModel;
            }

            set
            {
                DataContext = value;
            }
        }

        public TargetProjectView()
        {
            InitializeComponent();
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {

        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void UserControl_DragOver(object sender, DragEventArgs e)
        {

        }
    }
}
