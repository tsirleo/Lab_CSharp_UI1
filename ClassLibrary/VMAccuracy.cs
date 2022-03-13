using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class VMAccuracy
    {
        public VMGrid gridParam { get; private set; }

        public double sinMaxDiffMod { get; private set; }
        public double sinArgMaxDiff { get; private set; }
        public double sinValMaxDiff_HA { get; private set; }
        public double sinValMaxDiff_LA { get; private set; }
        public double sinValMaxDiff_EP { get; private set; }

        public double cosMaxDiffMod { get; private set; }
        public double cosArgMaxDiff { get; private set; }
        public double cosValMaxDiff_HA { get; private set; }
        public double cosValMaxDiff_LA { get; private set; }
        public double cosValMaxDiff_EP { get; private set; }

        public double scMaxDiffMod { get; private set; }
        public double scArgMaxDiff { get; private set; }
        public double scValMaxDiff_HA { get; private set; }
        public double scValMaxDiff_LA { get; private set; }
        public double scValMaxDiff_EP { get; private set; }

        public VMAccuracy(VMGrid gp)
        {
            gridParam = new VMGrid(gp);
        }

        public void MaxDiffModule(double[] valsHA, double[] valsEP)
        {
            if (valsEP != null && valsHA != null)
            {
                double maxmod = Math.Abs(valsHA[0] - valsEP[0]);
                for (int i = 1; i < valsHA.Length; i++)
                {
                    var temp = Math.Abs(valsHA[i] - valsEP[i]);
                    if (temp > maxmod)
                    {
                        maxmod = temp;
                    }
                }

                switch (gridParam.funcType)
                {
                    case VMf.vmdSin:
                        sinMaxDiffMod = maxmod;
                        break;
                    case VMf.vmdCos:
                        cosMaxDiffMod = maxmod;
                        break;
                    case VMf.vmdSinCos:
                        scMaxDiffMod = maxmod;
                        break;
                }
            }
        }

        public void ValArgMaxDiff(double[] args, double[] valsHA, double[] valsLA, double[] valsEP)
        {
            if (valsEP != null && valsHA != null && args != null)
            {
                double maxdiff = valsHA[0] - valsEP[0];
                int imax = 0;
                for (int i = 1; i < valsHA.Length; i++)
                {
                    var temp = valsHA[i] - valsEP[i];
                    if (temp > maxdiff)
                    {
                        maxdiff = temp;
                        imax = i;
                    }
                }

                switch (gridParam.funcType)
                {
                    case VMf.vmdSin:
                        sinArgMaxDiff = args[imax];
                        sinValMaxDiff_HA = valsHA[imax];
                        sinValMaxDiff_LA = valsLA[imax];
                        sinValMaxDiff_EP = valsEP[imax];
                        break;
                    case VMf.vmdCos:
                        cosArgMaxDiff = args[imax];
                        cosValMaxDiff_HA = valsHA[imax];
                        cosValMaxDiff_LA = valsLA[imax];
                        cosValMaxDiff_EP = valsEP[imax];
                        break;
                    case VMf.vmdSinCos:
                        scArgMaxDiff = args[imax];
                        scValMaxDiff_HA = valsHA[imax];
                        scValMaxDiff_LA = valsLA[imax];
                        scValMaxDiff_EP = valsEP[imax];
                        break;
                }
            }
        }

        public override string ToString()
        {
            switch (gridParam.funcType)
            {
                case VMf.vmdSin:
                    return $"VMAccuracy: GridInfo: nodeNum = {gridParam.length}\t segment = [{gridParam.segBounds[0]}, {gridParam.segBounds[1]}]\t" +
                           $"step = {gridParam.step}\n\tsinMaxDiffMod = {sinMaxDiffMod}\tsinArgMaxDiff = {sinArgMaxDiff}\t" +
                           $"sinValMaxDiff_HA = {sinValMaxDiff_HA}\tsinValMaxDiff_LA = {sinValMaxDiff_LA}\n\tsinValMaxDiff_EP = {sinValMaxDiff_EP}\n";
                case VMf.vmdCos:
                    return $"VMAccuracy: GridInfo: nodeNum = {gridParam.length}\t segment = [{gridParam.segBounds[0]}, {gridParam.segBounds[1]}]\t" +
                           $"step = {gridParam.step}\n\tcosMaxDiffMod = {cosMaxDiffMod}\tcosArgMaxDiff = {cosArgMaxDiff}\t" +
                           $"cosValMaxDiff_HA = {cosValMaxDiff_HA}\tcosValMaxDiff_LA = {cosValMaxDiff_LA}\n\tcosValMaxDiff_EP = {cosValMaxDiff_EP}\n";
                case VMf.vmdSinCos:
                    return $"VMAccuracy: GridInfo: nodeNum = {gridParam.length}\t segment = [{gridParam.segBounds[0]}, {gridParam.segBounds[1]}]\t" +
                           $"step = {gridParam.step}\n\tscMaxDiffMod = {scMaxDiffMod}\tscArgMaxDiff = {scArgMaxDiff}\t" +
                           $"scValMaxDiff_HA = {scValMaxDiff_HA}\tscValMaxDiff_LA = {scValMaxDiff_LA}\n\tscValMaxDiff_EP = {scValMaxDiff_EP}\n";
            }

            return "";
        }
    }
}
