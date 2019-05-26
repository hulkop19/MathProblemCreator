using MathProblemCreator.BussinessLogik;
using MathProblemCreator.BussinessLogik.Problems;
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
    /// Interaction logic for AddProblemWindow.xaml
    /// </summary>
    public partial class AddProblemWindow : Window
    {
        private List<ProblemInfo> _problemsInfo = DataProvider.GetProblemsInfo();
        private IProblem _newProblem = null;

        public AddProblemWindow()
        {
            InitializeComponent();
            Loaded += AddProblemWindow_Loaded;
        }

        private void AddProblemWindow_Loaded(object sender, RoutedEventArgs e)
        {
            problemNameComboBox.ItemsSource = _problemsInfo;
            problemNameComboBox.SelectedIndex = 0;
        }

        public static IProblem GetNewProblem()
        {
            var addProblemWindow = new AddProblemWindow();
            addProblemWindow.ShowDialog();

            return addProblemWindow._newProblem;
        }

        private IProblem GetProblemById(string id, string parametrs)
        {
            if (id == "EuclideanAlgorithmProblem")
            {
                return new EuclideanAlgorithmProblem(parametrs);
            }
            else if (id == "FiveDivProblem")
            {
                return new FiveDivProblem(parametrs);
            }
            else if (id == "IrreducibleFractionProblem")
            {
                return new IrreducibleFractionProblem(parametrs);
            }
            else if (id == "IsNumberSquareProblem")
            {
                return new IsNumberSquareProblem(parametrs);
            }

            throw new NotImplementedException();
        }

        private void ProblemNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int nameCbIndex = problemNameComboBox.SelectedIndex;
            problemDescriptionTextBox.Text = _problemsInfo[nameCbIndex].Description;

            int diffCbIndex = problemDifficultyComboBox.SelectedIndex;
            problemDifficultyComboBox.ItemsSource = _problemsInfo[nameCbIndex].ProblemParametrs.Select(item => item.Difficulty.Name);

            if (diffCbIndex < 0)
            {
                problemDifficultyComboBox.SelectedIndex = 0;
            }
            if (diffCbIndex >= 0)
            {
                problemDifficultyComboBox.SelectedIndex = -1;
                problemDifficultyComboBox.SelectedIndex = diffCbIndex;
            }
        }

        private void ProblemDifficultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int nameCbIndex = problemNameComboBox.SelectedIndex;
            int diffCbIndex = problemDifficultyComboBox.SelectedIndex;

            if (diffCbIndex >= 0)
            {
                problemDiffDescriptionTextBox.Text = _problemsInfo[nameCbIndex].ProblemParametrs[diffCbIndex].Difficulty.Discription;
            }
        }

        private void AddProblemBtn_Click(object sender, RoutedEventArgs e)
        {
            int piIndex = problemNameComboBox.SelectedIndex;
            int paramsIndex = problemDifficultyComboBox.SelectedIndex;

            string id = _problemsInfo[piIndex].Id;
            string parametrs = _problemsInfo[piIndex].ProblemParametrs[paramsIndex].Parametrs;

            _newProblem = GetProblemById(id, parametrs);

            this.Close();
        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            _newProblem = null;
            this.Close();
        }
    }
}
