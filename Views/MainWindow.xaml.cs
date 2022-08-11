using System.Windows;
using QueryConverter.ViewModels;

namespace QueryConverter.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ExecuteSqlProcedureConverterViewModel();
        }
    }
}