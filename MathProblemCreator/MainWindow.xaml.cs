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
using MathProblemCreator.BussinessLogik;
using Newtonsoft.Json;
using MathProblemCreator.MyWindows;
using System.IO;

namespace MathProblemCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindowLoaded;
            DataProvider.Initialize();
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            worksLb.ItemsSource = DataProvider.GetWorksList();
        }

        private void AddWorkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            var (workName, variantsNumber) = AddWorkWindow.GetWorkData();
            this.IsEnabled = true;

            if (workName != null && variantsNumber > 0)
            {
                worksLb.ItemsSource = WorkChanger.AddWork(new Work(workName, variantsNumber));
            }
        }

        private void NameTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var work = (Work)(((TextBlock)sender).DataContext);

            this.IsEnabled = false;
            Work changedWork = WorkEditorWindow.GetChangedWork(work.Id);
            this.IsEnabled = true;

            if (changedWork != null)
            {
                var works = DataProvider.GetWorksList();

                var changedWorkIndex = works.FindIndex(w => w.Id == work.Id);
                works[changedWorkIndex] = changedWork;
                DataProvider.SetWorkList(works);
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var work = (Work)worksLb.Items.GetItemAt(worksLb.SelectedIndex);

            this.IsEnabled = false;
            bool isConfrimed = DeleteWorkWindow.IsConfrimed();
            this.IsEnabled = true;

            if (isConfrimed)
            {
                worksLb.ItemsSource = WorkChanger.DeleteWork(work.Id);
            }
        }
    }
}
