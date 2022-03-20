using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class VMTime
    {
        public VMGrid gridParam { get; private set; }
        public double sinTime_HA { get; private set; }
        public double sinTime_LA { get; private set; }
        public double sinTime_EP { get; private set; }
        public double cosTime_HA { get; private set; }
        public double cosTime_LA { get; private set; }
        public double cosTime_EP { get; private set; }
        public double sinCosTime_HA { get; private set; }
        public double sinCosTime_LA { get; private set; }
        public double sinCosTime_EP { get; private set; }

        public VMTime(VMGrid gp, double HA, double LA, double EP)
        {
            gridParam = new VMGrid(gp);
            switch (gp.funcType)
            {
                case VMf.vmdSin:
                    sinTime_HA = HA;
                    sinTime_LA = LA;
                    sinTime_EP = EP;
                    break;
                case VMf.vmdCos:
                    cosTime_HA = HA;
                    cosTime_LA = LA;
                    cosTime_EP = EP;
                    break;
                case VMf.vmdSinCos:
                    sinCosTime_HA = HA;
                    sinCosTime_LA = LA;
                    sinCosTime_EP = EP;
                    break;
            }
        }

        public double LA_to_HA
        {
            get
            {
                switch (gridParam.funcType)
                {
                    case VMf.vmdSin:
                        return (sinTime_LA / sinTime_HA);
                    case VMf.vmdCos:
                        return (cosTime_LA / cosTime_HA);
                    case VMf.vmdSinCos:
                        return (sinCosTime_LA / sinCosTime_HA);
                }

                return 0;
            }
        }

        public double EP_to_HA
        {
            get
            {
                switch (gridParam.funcType)
                {
                    case VMf.vmdSin:
                        return (sinTime_EP / sinTime_HA);
                    case VMf.vmdCos:
                        return (cosTime_EP / cosTime_HA);
                    case VMf.vmdSinCos:
                        return (sinCosTime_EP / sinCosTime_HA);
                }

                return 0;
            }
        }

        public double[] RetArr
        {
            get
            {
                switch (gridParam.funcType)
                {
                    case VMf.vmdSin:
                        return new[] { sinTime_HA, sinTime_LA, sinTime_EP };
                    case VMf.vmdCos:
                        return new[] { cosTime_HA, cosTime_LA, cosTime_EP };
                    case VMf.vmdSinCos:
                        return new[] { sinCosTime_HA, sinCosTime_LA, sinCosTime_EP };
                }

                return null;
            }
        }

        public double RetHA
        {
            get
            {
                switch (gridParam.funcType)
                {
                    case VMf.vmdSin:
                        return sinTime_HA;
                    case VMf.vmdCos:
                        return cosTime_HA;
                    case VMf.vmdSinCos:
                        return sinCosTime_HA;
                }

                return 0;
            }
        }

        public double RetLA
        {
            get
            {
                switch (gridParam.funcType)
                {
                    case VMf.vmdSin:
                        return sinTime_LA;
                    case VMf.vmdCos:
                        return cosTime_LA;
                    case VMf.vmdSinCos:
                        return sinCosTime_LA;
                }

                return 0;
            }
        }

        public double RetEP
        {
            get
            {
                switch (gridParam.funcType)
                {
                    case VMf.vmdSin:
                        return sinTime_EP;
                    case VMf.vmdCos:
                        return cosTime_LA;
                    case VMf.vmdSinCos:
                        return sinCosTime_LA;
                }

                return 0;
            }
        }

        public override string ToString()
        {
            switch (gridParam.funcType)
            {
                case VMf.vmdSin:
                    return $"GridInfo:\n\tnodeNum = {gridParam.length}\n\tsegment = [{gridParam.segBounds[0]}; {gridParam.segBounds[1]}]\n\t" +
                           $"step = {gridParam.step}\n\tsinTime_HA = {sinTime_HA} seconds\n\tsinTime_LA = {sinTime_LA} seconds\n\t" +
                           $"sinTime_EP = {sinTime_EP} seconds\n\tLA_to_HA_ratio = {LA_to_HA}\n\tEP_to_HA_ratio = {EP_to_HA}\n";
                case VMf.vmdCos:
                    return $"GridInfo:\n\tnodeNum = {gridParam.length}\n\tsegment = [{gridParam.segBounds[0]}; {gridParam.segBounds[1]}]\n\t" +
                           $"step = {gridParam.step}\n\tcosTime_HA = {cosTime_HA} seconds\n\tcosTime_LA = {cosTime_LA} seconds\n\t" +
                           $"cosTime_EP = {cosTime_EP} seconds\n\tLA_to_HA_ratio = {LA_to_HA}\n\tEP_to_HA_ratio = {EP_to_HA}\n";
                case VMf.vmdSinCos:
                    return $"GridInfo:\n\tnodeNum = {gridParam.length}\n\tsegment = [{gridParam.segBounds[0]}; {gridParam.segBounds[1]}]\n\t" +
                           $"step = {gridParam.step}\n\tsinCosTime_HA = {sinCosTime_HA} seconds\n\tsinCosTime_LA = {sinCosTime_LA} seconds\n\t" +
                           $"sinCosTime_EP = {sinCosTime_EP} seconds\n\tLA_to_HA_ratio = {LA_to_HA}\n\tEP_to_HA_ratio = {EP_to_HA}\n";
            }

            return "";
        }
    }
}
