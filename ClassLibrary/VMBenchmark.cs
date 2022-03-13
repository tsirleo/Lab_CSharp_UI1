using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class VMBenchmark
    {
        ObservableCollection<VMTime> timeTestRes { get; set; }
        ObservableCollection<VMAccuracy> accComparRes { get; set; }

        public VMBenchmark()
        {
            timeTestRes = new ObservableCollection<VMTime>();
            accComparRes = new ObservableCollection<VMAccuracy>();
        }

        private double[] makeArgs(double head, double end, int N)
        {
            var step = (end - head) / N;
            double[] args = new double[N];
            args[0] = head;
            args[N - 1] = end;
            for (int i = 1; i < N - 1; i++)
            {
                args[i] = args[i - 1] + step;
            }

            return args;
        }

        public void AddVMTime(VMGrid grid)
        {
            double[] args = makeArgs(grid.segBounds[0], grid.segBounds[1], grid.length);
            double[] valsHA = new double[args.Length];
            double[] valsLA = new double[args.Length];
            double[] valsEP = new double[args.Length];
            int ret = -1;
            try
            {
                switch (grid.funcType)
                {
                    case VMf.vmdSin:
                        double sinTimeHA = 0.0, sinTimeLA = 0.0, sinTimeEP = 0.0;
                        VM_Sin(grid.length, args, valsHA, valsLA, valsEP, ref sinTimeHA, ref sinTimeLA, ref sinTimeEP, ref ret);
                        Console.WriteLine($"VM_Sin test: ret = {ret}");
                        timeTestRes.Add(new VMTime(grid, sinTimeHA, sinTimeLA, sinTimeEP));
                        break;
                    case VMf.vmdCos:
                        double cosTimeHA = 0.0, cosTimeLA = 0.0, cosTimeEP = 0.0;
                        VM_Cos(grid.length, args, valsHA, valsLA, valsEP, ref cosTimeHA, ref cosTimeLA, ref cosTimeEP, ref ret);
                        Console.WriteLine($"VM_Cos test: ret = {ret}");
                        timeTestRes.Add(new VMTime(grid, cosTimeHA, cosTimeLA, cosTimeEP));
                        break;
                    case VMf.vmdSinCos:
                        double[] tempValsHA = new double[args.Length];
                        double[] tempValsLA = new double[args.Length];
                        double[] tempValsEP = new double[args.Length];
                        double scTimeHA = 0.0, scTimeLA = 0.0, scTimeEP = 0.0;
                        VM_SinCos(grid.length, args, tempValsHA, tempValsLA, tempValsEP, valsHA, valsLA, valsEP, ref scTimeHA, ref scTimeLA, ref scTimeEP, ref ret);
                        Console.WriteLine($"VM_SinCos test: ret = {ret}");
                        timeTestRes.Add(new VMTime(grid, scTimeHA, scTimeLA, scTimeEP));
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void AddVMAccuracy(VMGrid grid)
        {
            double[] args = makeArgs(grid.segBounds[0], grid.segBounds[1], grid.length);
            double[] valsHA = new double[args.Length];
            double[] valsLA = new double[args.Length];
            double[] valsEP = new double[args.Length];
            int ret = -1;
            VMAccuracy acc = new VMAccuracy(grid);
            try
            {
                switch (grid.funcType)
                {
                    case VMf.vmdSin:
                        double sinTimeHA = 0.0, sinTimeLA = 0.0, sinTimeEP = 0.0;
                        VM_Sin(grid.length, args, valsHA, valsLA, valsEP, ref sinTimeHA, ref sinTimeLA, ref sinTimeEP, ref ret);
                        Console.WriteLine($"VM_Sin test: ret = {ret}");
                        acc.MaxDiffModule(valsHA, valsEP);
                        acc.ValArgMaxDiff(args, valsHA, valsLA, valsEP);
                        break;
                    case VMf.vmdCos:
                        double cosTimeHA = 0.0, cosTimeLA = 0.0, cosTimeEP = 0.0;
                        VM_Cos(grid.length, args, valsHA, valsLA, valsEP, ref cosTimeHA, ref cosTimeLA, ref cosTimeEP, ref ret);
                        Console.WriteLine($"VM_Cos test: ret = {ret}");
                        acc.MaxDiffModule(valsHA, valsEP);
                        acc.ValArgMaxDiff(args, valsHA, valsLA, valsEP);
                        break;
                    case VMf.vmdSinCos:
                        double[] tempValsHA = new double[args.Length];
                        double[] tempValsLA = new double[args.Length];
                        double[] tempValsEP = new double[args.Length];
                        double scTimeHA = 0.0, scTimeLA = 0.0, scTimeEP = 0.0;
                        VM_SinCos(grid.length, args, tempValsHA, tempValsLA, tempValsEP, valsHA, valsLA, valsEP, ref scTimeHA, ref scTimeLA, ref scTimeEP, ref ret);
                        Console.WriteLine($"VM_SinCos test: ret = {ret}");
                        acc.MaxDiffModule(valsHA, valsEP);
                        acc.ValArgMaxDiff(args, valsHA, valsLA, valsEP);
                        break;
                }
                accComparRes.Add(acc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public double minAll_EP_to_HA
        {
            get
            {
                if (timeTestRes != null)
                {
                    double min = timeTestRes[0].EP_to_HA;
                    foreach (var vt in timeTestRes)
                    {
                        if (min > vt.EP_to_HA)
                        {
                            min = vt.EP_to_HA;
                        }
                    }

                    return min;
                }

                return 0;
            }
        }

        public double minAll_LA_to_HA
        {
            get
            {
                if (timeTestRes != null)
                {
                    double min = timeTestRes[0].LA_to_HA;
                    foreach (var vt in timeTestRes)
                    {
                        if (min > vt.LA_to_HA)
                        {
                            min = vt.LA_to_HA;
                        }
                    }

                    return min;
                }

                return 0;
            }
        }

        public override string ToString()
        {
            if (timeTestRes != null && accComparRes != null)
            {
                string output = "";
                Console.WriteLine("\n***********************************************************");
                for (int k = 0; k < timeTestRes.Count; k++)
                {
                    output += $"Experiment number {k + 1}:\n" + timeTestRes[k].ToString() + accComparRes[k].ToString() +
                              "\n***********************************************************\n";
                }

                return output;
            }

            return "Sorry, all lists in benchmark class are empty.";
        }

        [DllImport("..\\..\\..\\..\\x64\\DEBUG\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VM_Sin(int n, double[] x, double[] yHA, double[] yLA, double[] yEP, ref double timeHA, ref double timeLA, ref double timeEP, ref int ret);
        [DllImport("..\\..\\..\\..\\x64\\DEBUG\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VM_Cos(int n, double[] x, double[] yHA, double[] yLA, double[] yEP, ref double timeHA, ref double timeLA, ref double timeEP, ref int ret);
        [DllImport("..\\..\\..\\..\\x64\\DEBUG\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VM_SinCos(int n, double[] x, double[] yHA, double[] yLA, double[] yEP, double[] zHA, double[] zLA, double[] zEP, ref double timeHA, ref double timeLA, ref double timeEP, ref int ret);
    }
}
