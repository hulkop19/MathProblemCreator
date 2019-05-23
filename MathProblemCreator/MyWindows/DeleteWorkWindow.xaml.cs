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

namespace MathProblemCreator.MyWindows
{
    /// <summary>
    /// Interaction logic for DeleteWorkWindow.xaml
    /// </summary>
    public partial class DeleteWorkWindow : Window
    {
        private bool _deletingIsConfrimed = false;

        public DeleteWorkWindow()
        {
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            _deletingIsConfrimed = true;
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static bool IsConfrimed()
        {
            var deleteWindow = new DeleteWorkWindow();
            deleteWindow.ShowDialog();

            return deleteWindow._deletingIsConfrimed;
        }
    }
}
