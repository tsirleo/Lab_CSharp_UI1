using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace CSharp_UI1
{
    [Serializable]
    class ViewData
    {
        public VMBenchmark bchmark;

        public ViewData()
        {
            bchmark = new VMBenchmark();
        }

        public void AddVMTime(VMGrid grid)
        {
            bchmark.AddVMTime(grid);
        }

        public void AddVMAccuracy(VMGrid grid)
        {
            bchmark.AddVMAccuracy(grid);
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
                System.Windows.MessageBox.Show($"Error saving binary file: {x}");
                return false;
            }
            finally
            {
                if (fStrm != null) { fStrm.Close(); }
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
                System.Windows.MessageBox.Show($"Error loading binary file: {x}");
                return false;
            }
            finally
            {
                if (fStrm != null) { fStrm.Close(); }
            }
        }
    }
}
