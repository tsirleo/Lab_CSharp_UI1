using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClassLibrary;

namespace CSharp_UI1
{
    [Serializable]
    public class ViewData
    {
        public VMBenchmark bchmark;
        public bool isChanged { get; set; }

        [field: NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnCollectionChanged(NotifyCollectionChangedAction ev)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ViewData()
        {
            bchmark = new VMBenchmark();
            isChanged = false;
        }

        public void AddVMTime(VMGrid grid)
        {
            if (bchmark != null)
            {
                bchmark.AddVMTime(grid);
                OnCollectionChanged(NotifyCollectionChangedAction.Add);
                isChanged = true;
                OnPropertyChanged("isChanged");
            }
        }

        public void AddVMAccuracy(VMGrid grid)
        {
            if (bchmark != null)
            {
                bchmark.AddVMAccuracy(grid);
                OnCollectionChanged(NotifyCollectionChangedAction.Add);
                isChanged = true;
                OnPropertyChanged("isChanged");
            }
        }

        public bool SaveBinary(string filename)
        {
            FileStream fStrm = null;
            try
            {
                fStrm = File.Create(filename);
                BinaryFormatter binForm = new BinaryFormatter();
                binForm.Serialize(fStrm, this);
                return true;
            }
            catch (Exception x)
            {
                System.Windows.MessageBox.Show($"An error occurred while saving data: {x}", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (fStrm != null) { fStrm.Close(); }

                isChanged = false;
                OnPropertyChanged("isChanged");
            }
        }

        public bool LoadBinary(string filename)
        {
            FileStream fStrm = null;
            try
            {
                fStrm = File.OpenRead(filename);
                BinaryFormatter binForm = new BinaryFormatter();
                bchmark = new VMBenchmark();
                bchmark = binForm.Deserialize(fStrm) as VMBenchmark;
                return true;
            }
            catch (Exception x)
            {
                System.Windows.MessageBox.Show($"An error occurred while loading data: {x}", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (fStrm != null) { fStrm.Close(); }

                isChanged = false;
                OnPropertyChanged("isChanged");
            }
        }
    }
}
