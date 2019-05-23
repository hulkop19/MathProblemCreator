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
    /// Interaction logic for AddWorkWindow.xaml
    /// </summary>
    public partial class AddWorkWindow : Window
    {
        private string _workName = null;
        private int _variantsNumber = 0;

        public AddWorkWindow()
        {
            InitializeComponent();
        }

        private void WorkTextboxes_TextChanged(object sender, RoutedEventArgs e)
        {
            int variantsNumber = 0;
            if (!string.IsNullOrWhiteSpace(workNameTextbox.Text)
                && !string.IsNullOrWhiteSpace(workVariantsNumberTextbox.Text)
                && int.TryParse(workVariantsNumberTextbox.Text, out variantsNumber)
                && variantsNumber > 0)
            {
                addWorkBtn.IsEnabled = true;
            }
            else
            {
                addWorkBtn.IsEnabled = false;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddWorkBtn_Click(object sender, RoutedEventArgs e)
        {
            _workName = workNameTextbox.Text;
            _variantsNumber = int.Parse(workVariantsNumberTextbox.Text);
            this.Close();
        }

        public static (string Name, int VariantsNumber) GetWorkData()
        {
            AddWorkWindow window = new AddWorkWindow();
            window.ShowDialog();

            return (window._workName, window._variantsNumber);
        }
    }
}
