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
        public int ndNum = 1;
        public double sgstart = 0.0;
        public double sgend = 0.0;
        public VMf fctype = VMf.vmdSin;

        public MainWindow()
        {
            InitializeComponent();
            VMTgrid.DataContext = benchmark.bchmark.timeTestRes;
            VMAgrid.DataContext = benchmark.bchmark.accComparRes;
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
            if (benchmark != null && ndNum != 1 && sgstart != 0.0 && sgend != 0.0)
            {
               // System.Windows.MessageBox.Show($"{ndNum}    {sgstart}   {sgend} {fctype}", "values", MessageBoxButton.OK, MessageBoxImage.Information);
                benchmark.AddVMTime(new VMGrid(ndNum, sgstart, sgend, fctype));
                benchmark.isChanged = true;
                infoblock.Text = DateTime.Now + "\n" + "New element VMtime is successfully added to collection!";
                minRelation.Text = $"\n\tminAll_EP_to_HA = {benchmark.bchmark.minAll_EP_to_HA}\n\tminAll_LA_to_HA = {benchmark.bchmark.minAll_LA_to_HA}";
            }
            else
            {
                System.Windows.MessageBox.Show("Benchmark is null. Create new one.\nOr check correctness of your data!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddVMAMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (benchmark != null && ndNum != 1 && sgstart != 0.0 && sgend != 0.0)
            {
                benchmark.AddVMAccuracy(new VMGrid(ndNum, sgstart, sgend, fctype));
                benchmark.isChanged = true;
                infoblock.Text = DateTime.Now + "\n" + "New element VMAccuracy is successfully added to collection!";
                minRelation.Text = $"\n\tminAll_EP_to_HA = {benchmark.bchmark.minAll_EP_to_HA}\n\tminAll_LA_to_HA = {benchmark.bchmark.minAll_LA_to_HA}";
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

        private void nodeNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ndNum = int.Parse(nodeNum.Text);
                if (ndNum <= 0)
                {
                    System.Windows.MessageBox.Show($"The number of nodes must be greater than zero!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                if (nodeNum.Text != "")
                {
                    System.Windows.MessageBox.Show($"Node number must be integer!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void segstart_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sgstart = double.Parse(segstart.Text);
            }
            catch (Exception ex)
            {
                if (segstart.Text != "")
                {
                    System.Windows.MessageBox.Show($"Segment params must be double!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void segend_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sgend = double.Parse(segend.Text);
                if (sgstart >= sgend)
                {
                    System.Windows.MessageBox.Show($"Segment end value must be greater than begin value!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                if (segend.Text != "")
                {
                    System.Windows.MessageBox.Show($"Segment params must be double!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void funcType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (funcType.Text)
            {
                case "vmdSin":
                    fctype = VMf.vmdSin;
                    break;
                case "vmdCos":
                    fctype = VMf.vmdCos;
                    break;
                case "vmdSinCos":
                    fctype = VMf.vmdSinCos;
                    break;
            }
        }

        private void VMTListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                VMTTextBlock.Text = VMTListBox.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Element is empty");
            }
        }

        private void VMAListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VMATextBlock.Text = VMAListBox.SelectedItem.ToString();
        }
    }
}
