﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
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

        public VMAccuracy(VMGrid gp, double mdf, double amf, double vmf_HA, double vmf_LA, double vmf_EP)
        {
            gridParam = new VMGrid(gp);
            switch (gridParam.funcType)
            {
                case VMf.vmdSin:
                    sinMaxDiffMod = mdf;
                    sinArgMaxDiff = amf;
                    sinValMaxDiff_HA = vmf_HA;
                    sinValMaxDiff_LA = vmf_LA;
                    sinValMaxDiff_EP = vmf_EP;
                    break;
                case VMf.vmdCos:
                    cosMaxDiffMod = mdf;
                    cosArgMaxDiff = amf;
                    cosValMaxDiff_HA = vmf_HA;
                    cosValMaxDiff_LA = vmf_LA;
                    cosValMaxDiff_EP = vmf_EP;
                    break;
                case VMf.vmdSinCos:
                    scMaxDiffMod = mdf;
                    scArgMaxDiff = amf;
                    scValMaxDiff_HA = vmf_HA;
                    scValMaxDiff_LA = vmf_LA;
                    scValMaxDiff_EP = vmf_EP;
                    break;
            }
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
                    return $"GridInfo:\n\tnodeNum = {gridParam.length}\n\tsegment = [{gridParam.segBounds[0]}; {gridParam.segBounds[1]}]\n\t" +
                           $"step = {gridParam.step}\n\tsinMaxDiffMod = {sinMaxDiffMod}\n\tsinArgMaxDiff = {sinArgMaxDiff}\n\t" +
                           $"sinValMaxDiff_HA = {sinValMaxDiff_HA}\n\tsinValMaxDiff_LA = {sinValMaxDiff_LA}\n\tsinValMaxDiff_EP = {sinValMaxDiff_EP}\n";
                case VMf.vmdCos:
                    return $"GridInfo:\n\tnodeNum = {gridParam.length}\n\tsegment = [{gridParam.segBounds[0]}; {gridParam.segBounds[1]}]\n\t" +
                           $"step = {gridParam.step}\n\tcosMaxDiffMod = {cosMaxDiffMod}\n\tcosArgMaxDiff = {cosArgMaxDiff}\n\t" +
                           $"cosValMaxDiff_HA = {cosValMaxDiff_HA}\n\tcosValMaxDiff_LA = {cosValMaxDiff_LA}\n\tcosValMaxDiff_EP = {cosValMaxDiff_EP}\n";
                case VMf.vmdSinCos:
                    return $"GridInfo:\n\tnodeNum = {gridParam.length}\n\tsegment = [{gridParam.segBounds[0]}; {gridParam.segBounds[1]}]\n\t" +
                           $"step = {gridParam.step}\n\tscMaxDiffMod = {scMaxDiffMod}\n\tscArgMaxDiff = {scArgMaxDiff}\n\t" +
                           $"scValMaxDiff_HA = {scValMaxDiff_HA}\n\tscValMaxDiff_LA = {scValMaxDiff_LA}\n\tscValMaxDiff_EP = {scValMaxDiff_EP}\n";
            }

            return "";
        }
    }
}
