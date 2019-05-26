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
using MathProblemCreator.BussinessLogik;
using MathProblemCreator.BussinessLogik.Problems;
using Microsoft.Win32;

namespace MathProblemCreator.MyWindows
{
    /// <summary>
    /// Interaction logic for WorkEditorWindow.xaml
    /// </summary>
    public partial class WorkEditorWindow : Window
    {
        private Work _work = null;

        public WorkEditorWindow(int workId)
        {
            _work = DataProvider.GetWorksList().Find((work) => work.Id == workId);
            InitializeComponent();
            Loaded += WorkEditorWindowLoaded;
        }

        private void WorkEditorWindowLoaded(object sender, RoutedEventArgs e)
        {
            problemsLb.ItemsSource = _work.Variants[0].Select(p => new { Problem = p.Problem, Answer = p.Answer});
            variantsComboBox.ItemsSource = Enumerable.Range(1, _work.Variants.Count);
        }

        public static Work GetChangedWork(int workId)
        {
            var workEditorWindow = new WorkEditorWindow(workId);
            workEditorWindow.ShowDialog();

            return workEditorWindow._work;
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _work.DeleteProblem(problemsLb.SelectedIndex);

            problemsLb.ItemsSource = null;
            problemsLb.ItemsSource = _work.Variants[variantsComboBox.SelectedIndex].Select(p => new { Problem = p.Problem, Answer = p.Answer });
        }

        private void VariantsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            problemsLb.ItemsSource = _work.Variants[variantsComboBox.SelectedIndex].Select(p => new { Problem = p.Problem, Answer = p.Answer });
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _work = null;
            this.Close();
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProblemDownBtn_Click(object sender, RoutedEventArgs e)
        {
            MoveProblem(1);
        }

        private void ProblemUpBtn_Click(object sender, RoutedEventArgs e)
        {
            MoveProblem(-1);
        }

        private void MoveProblem(int offset)
        {
            int index = problemsLb.SelectedIndex;

            if (offset < 0)
            {
                if (index + offset < 0) return;
            }
            else
            {
                if (index + offset >= _work.Variants[0].Count) return;
            }
            
            for (int i = 0; i < _work.Variants.Count; ++i)
            {
                var tmp = _work.Variants[i][index];
                _work.Variants[i][index] = _work.Variants[i][index + offset];
                _work.Variants[i][index + offset] = tmp;
            }

            problemsLb.ItemsSource = null;
            problemsLb.ItemsSource = _work.Variants[variantsComboBox.SelectedIndex].Select(p => new { Problem = p.Problem, Answer = p.Answer });
            problemsLb.SelectedIndex = index + offset;
        }

        private void AddProblemBtn_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            IProblem newProblem = AddProblemWindow.GetNewProblem();
            this.IsEnabled = true;

            if (newProblem != null)
            {
                for (int i = 0; i < _work.Variants.Count; ++i)
                {
                    _work.Variants[i].Add(newProblem.Generate());
                }

                problemsLb.ItemsSource = null;
                problemsLb.ItemsSource = _work.Variants[variantsComboBox.SelectedIndex].Select(p => new { Problem = p.Problem, Answer = p.Answer });
            }
        }

        private void CreateProblemsDocBtn_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            var saveFileWindow = new SaveFileDialog();
            saveFileWindow.DefaultExt = ".docx";
            var isSuccess = saveFileWindow.ShowDialog();
            this.IsEnabled = true;

            if (isSuccess == true && saveFileWindow.ValidateNames)
            {
                DataProvider.CreateDoc(_work, saveFileWindow.FileName, false);
            }
        }

        private void CreateAnswersDocBtn_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            var saveFileWindow = new SaveFileDialog();
            saveFileWindow.DefaultExt = ".docx";
            var isSuccess = saveFileWindow.ShowDialog();
            this.IsEnabled = true;

            if (isSuccess == true && saveFileWindow.ValidateNames)
            {
                DataProvider.CreateDoc(_work, saveFileWindow.FileName, true);
            }
        }
    }
}
