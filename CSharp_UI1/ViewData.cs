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
using Accessibility;
using ClassLibrary;

namespace CSharp_UI1
{
    public class ViewData: INotifyPropertyChanged
    {
        public VMBenchmark bchmark;
        public VMGrid Grid { get; set; } = new VMGrid(0, 0.0, 0.0, VMf.vmdSin);
        public bool isChanged { get; set; }
        private double mEPHA;
        private double mLAHA;
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewData()
        {
            bchmark = new VMBenchmark();
            isChanged = false;
            bchmark.timeTestRes.CollectionChanged += TMChanged;
        }

        private void TMChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            mEPHA = bchmark.minAll_EP_to_HA;
            mLAHA = bchmark.minAll_LA_to_HA;
            bchmark.minAll_EP_to_HA = mEPHA;
            bchmark.minAll_LA_to_HA = mLAHA;
        }

        public void AddVMTime(VMGrid grid)
        {
            if (bchmark != null)
            {
                bchmark.AddVMTime(grid);
                isChanged = true;
            }
        }

        public void AddVMAccuracy(VMGrid grid)
        {
            if (bchmark != null)
            {
                bchmark.AddVMAccuracy(grid);
                isChanged = true;
            }
        }

        public bool SaveBinary(string filename)
        {
            FileStream fStrm = null;
            StreamWriter sWrt = null;
            try
            {
                fStrm = File.Create(filename);
                sWrt = new StreamWriter(fStrm);

                sWrt.WriteLine(bchmark.timeTestRes.Count.ToString());
                sWrt.WriteLine(bchmark.accComparRes.Count.ToString());

                foreach (var elem in bchmark.timeTestRes)
                {
                    sWrt.WriteLine(elem.gridParam.length.ToString());
                    sWrt.WriteLine(elem.gridParam.segBounds[0].ToString());
                    sWrt.WriteLine(elem.gridParam.segBounds[1].ToString());
                    sWrt.WriteLine(elem.gridParam.funcType.ToString());
                    switch (elem.gridParam.funcType)
                    {
                        case VMf.vmdSin:
                            sWrt.WriteLine(elem.sinTime_HA.ToString());
                            sWrt.WriteLine(elem.sinTime_LA.ToString());
                            sWrt.WriteLine(elem.sinTime_EP.ToString());
                            break;
                        case VMf.vmdCos:
                            sWrt.WriteLine(elem.cosTime_HA.ToString());
                            sWrt.WriteLine(elem.cosTime_LA.ToString());
                            sWrt.WriteLine(elem.cosTime_EP.ToString());
                            break;
                        case VMf.vmdSinCos:
                            sWrt.WriteLine(elem.sinCosTime_HA.ToString());
                            sWrt.WriteLine(elem.sinCosTime_LA.ToString());
                            sWrt.WriteLine(elem.sinCosTime_EP.ToString());
                            break;
                    }
                }

                foreach (var elem in bchmark.accComparRes)
                {
                    sWrt.WriteLine(elem.gridParam.length.ToString());
                    sWrt.WriteLine(elem.gridParam.segBounds[0].ToString());
                    sWrt.WriteLine(elem.gridParam.segBounds[1].ToString());
                    sWrt.WriteLine(elem.gridParam.funcType.ToString());
                    switch (elem.gridParam.funcType)
                    {
                        case VMf.vmdSin:
                            sWrt.WriteLine(elem.sinMaxDiffMod.ToString());
                            sWrt.WriteLine(elem.sinArgMaxDiff.ToString());
                            sWrt.WriteLine(elem.sinValMaxDiff_HA.ToString());
                            sWrt.WriteLine(elem.sinValMaxDiff_LA.ToString());
                            sWrt.WriteLine(elem.sinValMaxDiff_EP.ToString());
                            break;
                        case VMf.vmdCos:
                            sWrt.WriteLine(elem.cosMaxDiffMod.ToString());
                            sWrt.WriteLine(elem.cosArgMaxDiff.ToString());
                            sWrt.WriteLine(elem.cosValMaxDiff_HA.ToString());
                            sWrt.WriteLine(elem.cosValMaxDiff_LA.ToString());
                            sWrt.WriteLine(elem.cosValMaxDiff_EP.ToString());
                            break;
                        case VMf.vmdSinCos:
                            sWrt.WriteLine(elem.scMaxDiffMod.ToString());
                            sWrt.WriteLine(elem.scArgMaxDiff.ToString());
                            sWrt.WriteLine(elem.scValMaxDiff_HA.ToString());
                            sWrt.WriteLine(elem.scValMaxDiff_LA.ToString());
                            sWrt.WriteLine(elem.scValMaxDiff_EP.ToString());
                            break;
                    }
                }

                return true;
            }
            catch (Exception x)
            {
                System.Windows.MessageBox.Show($"An error occurred while saving data: {x}", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (sWrt != null) { sWrt.Dispose(); }
                if (fStrm != null) { fStrm.Close(); }
                isChanged = false;
            }
        }

        public bool LoadBinary(string filename)
        {
            FileStream fStrm = null;
            StreamReader sRdr = null;
            VMf fctype;
            double sinTime_HA;
            double sinTime_LA;
            double sinTime_EP;
            double cosTime_HA;
            double cosTime_LA;
            double cosTime_EP;
            double sinCosTime_HA;
            double sinCosTime_LA;
            double sinCosTime_EP;
            double sinMaxDiffMod;
            double sinArgMaxDiff;
            double sinValMaxDiff_HA;
            double sinValMaxDiff_LA;
            double sinValMaxDiff_EP;
            double cosMaxDiffMod;
            double cosArgMaxDiff;
            double cosValMaxDiff_HA;
            double cosValMaxDiff_LA;
            double cosValMaxDiff_EP;
            double scMaxDiffMod;
            double scArgMaxDiff;
            double scValMaxDiff_HA;
            double scValMaxDiff_LA; 
            double scValMaxDiff_EP; 
            try
            {
                fStrm = File.OpenRead(filename);
                sRdr = new StreamReader(fStrm);
                bchmark = new VMBenchmark();

                int tml = Convert.ToInt32(sRdr.ReadLine());
                int acl = Convert.ToInt32(sRdr.ReadLine());

                for (int i = 0; i < tml; i++)
                {
                    int length = Convert.ToInt32(sRdr.ReadLine());
                    double segstart = Convert.ToDouble(sRdr.ReadLine());
                    double segend = Convert.ToDouble(sRdr.ReadLine());
                    switch (sRdr.ReadLine())
                    {
                        case "vmdSin":
                            fctype = VMf.vmdSin;
                            sinTime_HA = Convert.ToDouble(sRdr.ReadLine());
                            sinTime_LA = Convert.ToDouble(sRdr.ReadLine());
                            sinTime_EP = Convert.ToDouble(sRdr.ReadLine());
                            bchmark.timeTestRes.Add(new VMTime(new VMGrid(length, segstart, segend, fctype), sinTime_HA, sinTime_LA, sinTime_EP));
                            break;
                        case "vmdCos":
                            fctype = VMf.vmdCos;
                            cosTime_HA = Convert.ToDouble(sRdr.ReadLine());
                            cosTime_LA = Convert.ToDouble(sRdr.ReadLine());
                            cosTime_EP = Convert.ToDouble(sRdr.ReadLine());
                            bchmark.timeTestRes.Add(new VMTime(new VMGrid(length, segstart, segend, fctype), cosTime_HA, cosTime_LA, cosTime_EP));
                            break;
                        case "vmdSinCos":
                            fctype = VMf.vmdSinCos;
                            sinCosTime_HA = Convert.ToDouble(sRdr.ReadLine());
                            sinCosTime_LA = Convert.ToDouble(sRdr.ReadLine());
                            sinCosTime_EP = Convert.ToDouble(sRdr.ReadLine());
                            bchmark.timeTestRes.Add(new VMTime(new VMGrid(length, segstart, segend, fctype), sinCosTime_HA, sinCosTime_LA, sinCosTime_EP));
                            break;
                    }
                }
                bchmark.timeTestRes.CollectionChanged += TMChanged;
                for (int i = 0; i < acl; i++)
                {
                    int length = Convert.ToInt32(sRdr.ReadLine());
                    double segstart = Convert.ToDouble(sRdr.ReadLine());
                    double segend = Convert.ToDouble(sRdr.ReadLine());
                    switch (sRdr.ReadLine())
                    {
                        case "vmdSin":
                            fctype = VMf.vmdSin;
                            sinMaxDiffMod = Convert.ToDouble(sRdr.ReadLine());
                            sinArgMaxDiff = Convert.ToDouble(sRdr.ReadLine());
                            sinValMaxDiff_HA = Convert.ToDouble(sRdr.ReadLine());
                            sinValMaxDiff_LA = Convert.ToDouble(sRdr.ReadLine());
                            sinValMaxDiff_EP = Convert.ToDouble(sRdr.ReadLine());
                            bchmark.accComparRes.Add(new VMAccuracy(new VMGrid(length, segstart, segend, fctype), sinMaxDiffMod, sinArgMaxDiff, sinValMaxDiff_HA, sinValMaxDiff_LA, sinValMaxDiff_EP));
                            break;
                        case "vmdCos":
                            fctype = VMf.vmdCos;
                            cosMaxDiffMod = Convert.ToDouble(sRdr.ReadLine());
                            cosArgMaxDiff = Convert.ToDouble(sRdr.ReadLine());
                            cosValMaxDiff_HA = Convert.ToDouble(sRdr.ReadLine());
                            cosValMaxDiff_LA = Convert.ToDouble(sRdr.ReadLine());
                            cosValMaxDiff_EP = Convert.ToDouble(sRdr.ReadLine());
                            bchmark.accComparRes.Add(new VMAccuracy(new VMGrid(length, segstart, segend, fctype), cosMaxDiffMod, cosArgMaxDiff, cosValMaxDiff_HA, cosValMaxDiff_LA, cosValMaxDiff_EP));
                            break;
                        case "vmdSinCos":
                            fctype = VMf.vmdSinCos;
                            scMaxDiffMod = Convert.ToDouble(sRdr.ReadLine());
                            scArgMaxDiff = Convert.ToDouble(sRdr.ReadLine());
                            scValMaxDiff_HA = Convert.ToDouble(sRdr.ReadLine());
                            scValMaxDiff_LA = Convert.ToDouble(sRdr.ReadLine());
                            scValMaxDiff_EP = Convert.ToDouble(sRdr.ReadLine());
                            bchmark.accComparRes.Add(new VMAccuracy(new VMGrid(length, segstart, segend, fctype), scMaxDiffMod, scArgMaxDiff, scValMaxDiff_HA, scValMaxDiff_LA, scValMaxDiff_EP));
                            break;
                    }
                }

                return true;
            }
            catch (Exception x)
            {
                System.Windows.MessageBox.Show($"An error occurred while loading data: {x}", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (sRdr != null) { sRdr.Dispose(); }
                if (fStrm != null) { fStrm.Close(); }
                isChanged = false;
            }
        }
    }
}
