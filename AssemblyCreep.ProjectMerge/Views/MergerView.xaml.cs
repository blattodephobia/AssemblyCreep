using AssemblyCreep.ViewModels;
using System.Windows.Controls;

namespace AssemblyCreep.Views
{
    /// <summary>
    /// Interaction logic for MergerView.xaml
    /// </summary>
    public partial class MergerView : UserControl
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

        public MergerView()
        {
            InitializeComponent();
        }
    }
}
