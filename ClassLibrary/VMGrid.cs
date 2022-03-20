using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class VMGrid
    {
        public int length { get; private set; }
        public double[] segBounds { get; private set; }
        public double step { get; private set; }
        public VMf funcType { get; private set; }

        public VMGrid(int len, double head, double end, VMf ft)
        {
            length = len;
            segBounds = new double[2] { head, end };
            step = (end - head) / len;
            funcType = ft;
        }

        public VMGrid(VMGrid obj)
        {
            length = obj.length;
            segBounds = new double[2];
            segBounds = obj.segBounds;
            step = obj.step;
            funcType = obj.funcType;
        }

        public override string ToString()
        {
            return $"VMTime: GridInfo: nodeNum = {length}\t segment = [{segBounds[0]}, {segBounds[1]}]\t step = {step}\n";
        }
    }
}
