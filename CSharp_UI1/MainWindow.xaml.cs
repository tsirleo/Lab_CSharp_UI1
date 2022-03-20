using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ClassLibrary;

namespace CSharp_UI1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewData benchmark = new ViewData();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (benchmark != null && benchmark.isChanged)
            {
                var result = MessageBox.Show("All unsaved data will be lost!\nDo you wanna save your data?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);

                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.FileName = "BenchmarkData";
                    dlg.DefaultExt = ".txt";
                    dlg.Filter = "Text documents (.txt)|*.txt";

                    Nullable<bool> res = dlg.ShowDialog();

                    if (res == true)
                    {
                        string filename = dlg.FileName;
                        if (benchmark != null)
                        {
                            benchmark.SaveBinary(filename);
                            benchmark.isChanged = false;
                            infoblock.Text = DateTime.Now + "\n" + "Data is successfully saved!";
                        }
                    }
                }
                benchmark = new ViewData();
                infoblock.Text = DateTime.Now + "\n" + "New data is created!";
                VMTgrid.DataContext = benchmark.bchmark.timeTestRes;
                VMAgrid.DataContext = benchmark.bchmark.accComparRes;
            }
            else
            {
                benchmark = new ViewData();
                infoblock.Text = DateTime.Now + "\n" + "New data is created!";
                VMTgrid.DataContext = benchmark.bchmark.timeTestRes;
                VMAgrid.DataContext = benchmark.bchmark.accComparRes;
            }
        }

        private void OpenMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (benchmark != null && benchmark.isChanged)
            {
                var result = MessageBox.Show("All unsaved data will be lost!\nDo you wanna save your data?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);

                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.FileName = "BenchmarkData";
                    dlg.DefaultExt = ".txt";
                    dlg.Filter = "Text documents (.txt)|*.txt";

                    Nullable<bool> res = dlg.ShowDialog();

                    if (res == true)
                    {
                        string filename = dlg.FileName;
                        if (benchmark != null)
                        {
                            benchmark.SaveBinary(filename);
                            benchmark.isChanged = false;
                            infoblock.Text = DateTime.Now + "\n" + "Data is successfully saved!";
                        }
                    }

                    Microsoft.Win32.OpenFileDialog dialg = new Microsoft.Win32.OpenFileDialog();
                    dialg.FileName = "BenchmarkData";
                    dialg.DefaultExt = ".txt";
                    dialg.Filter = "Text documents (.txt)|*.txt";

                    Nullable<bool> reslt = dialg.ShowDialog();

                    if (reslt == true)
                    {
                        string filename = dialg.FileName;
                        if (benchmark != null)
                        {
                            benchmark.LoadBinary(filename);
                            infoblock.Text = DateTime.Now + "\n" + "Data is successfully loaded!";
                            VMTgrid.DataContext = benchmark.bchmark.timeTestRes;
                            VMAgrid.DataContext = benchmark.bchmark.accComparRes;
                            benchmark.isChanged = false;
                        }
                    }
                }
                else
                {
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.FileName = "BenchmarkData";
                    dlg.DefaultExt = ".txt";
                    dlg.Filter = "Text documents (.txt)|*.txt";

                    Nullable<bool> res = dlg.ShowDialog();

                    if (res == true)
                    {
                        string filename = dlg.FileName;
                        if (benchmark != null)
                        {
                            benchmark.LoadBinary(filename);
                            benchmark.isChanged = false;
                            infoblock.Text = DateTime.Now + "\n" + "Data is successfully loaded!";
                            VMTgrid.DataContext = benchmark.bchmark.timeTestRes;
                            VMAgrid.DataContext = benchmark.bchmark.accComparRes;
                            benchmark.isChanged = false;
                        }
                    }
                }
            }
            else
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "BenchmarkData";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents (.txt)|*.txt";

                Nullable<bool> res = dlg.ShowDialog();

                if (res == true)
                {
                    string filename = dlg.FileName;
                    if (benchmark != null)
                    {
                        benchmark.LoadBinary(filename);
                        benchmark.isChanged = false;
                        infoblock.Text = DateTime.Now + "\n" + "Data is successfully loaded!";
                        VMTgrid.DataContext = benchmark.bchmark.timeTestRes;
                        VMAgrid.DataContext = benchmark.bchmark.accComparRes;
                        benchmark.isChanged = false;
                    }
                }
            }
        }

        private void SaveMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "BenchmarkData"; 
            dlg.DefaultExt = ".txt"; 
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                if (benchmark != null)
                {
                    benchmark.SaveBinary(filename);
                    infoblock.Text = DateTime.Now + "\n" + "Data is successfully saved!";
                }

                benchmark.isChanged = false;
            }
        }

        private void AddVMTMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (benchmark != null && benchmark.Grid.length != 0)
            {
                if (benchmark.Grid.length <= 0) {
                    System.Windows.MessageBox.Show("Number of nodes must be greater than zero", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (benchmark.Grid.segBounds[0] > benchmark.Grid.segBounds[1])
                {
                    System.Windows.MessageBox.Show("End of segment must be greater than beginning", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (benchmark.Grid.length == 1 && (benchmark.Grid.segBounds[0] != benchmark.Grid.segBounds[1]))
                {
                    System.Windows.MessageBox.Show("When number of nodes is equal to one, the boundaries of the segment must match", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    benchmark.AddVMTime(new VMGrid(benchmark.Grid.length, benchmark.Grid.segBounds[0], benchmark.Grid.segBounds[1], benchmark.Grid.funcType));
                    benchmark.isChanged = true;
                    infoblock.Text = DateTime.Now + "\n" + "New element VMtime is successfully added to collection!";
                    //MinRelationBlock.Text = $"\n\tminAll_EP_to_HA = {benchmark.bchmark.minAll_EP_to_HA}\n\tminAll_LA_to_HA = {benchmark.bchmark.minAll_LA_to_HA}";
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Benchmark is null. Create new one.\nOr check correctness of your data!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddVMAMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (benchmark != null && benchmark.Grid.length != 0)
            {
                if (benchmark.Grid.length <= 0)
                {
                    System.Windows.MessageBox.Show("Number of nodes must be greater than zero", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (benchmark.Grid.segBounds[0] > benchmark.Grid.segBounds[1])
                {
                    System.Windows.MessageBox.Show("End of segment must be greater than beginning", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (benchmark.Grid.length == 1 && (benchmark.Grid.segBounds[0] != benchmark.Grid.segBounds[1]))
                {
                    System.Windows.MessageBox.Show("When number of nodes is equal to one, the boundaries of the segment must match", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    benchmark.AddVMAccuracy(new VMGrid(benchmark.Grid.length, benchmark.Grid.segBounds[0], benchmark.Grid.segBounds[1], benchmark.Grid.funcType));
                    benchmark.isChanged = true;
                    infoblock.Text = DateTime.Now + "\n" + "New element VMAccuracy is successfully added to collection!";
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Benchmark is null. Create new one.\nOr check correctness of your data!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (benchmark != null && benchmark.isChanged)
            {
                var result = MessageBox.Show("All unsaved data will be lost!\nDo you wanna save your data?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);

                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.FileName = "BenchmarkData";
                    dlg.DefaultExt = ".txt";
                    dlg.Filter = "Text documents (.txt)|*.txt";

                    Nullable<bool> res = dlg.ShowDialog();

                    if (res == true)
                    {
                        string filename = dlg.FileName;
                        if (benchmark != null)
                        {
                            benchmark.SaveBinary(filename);
                        }
                    }
                }
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            FuncTypeBox.ItemsSource = Enum.GetValues(typeof(VMf));
            VMTgrid.DataContext = benchmark.bchmark.timeTestRes;
            VMAgrid.DataContext = benchmark.bchmark.accComparRes;
            SegendBox.DataContext = benchmark.Grid;
            SegstartBox.DataContext = benchmark.Grid;
            FuncTypeBox.DataContext = benchmark.Grid;
            NodeNumBox.DataContext = benchmark.Grid;
            MinRelationBlock.DataContext = benchmark.bchmark;
        }
    }
}
