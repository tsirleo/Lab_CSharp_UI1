using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class VMGrid
    {
        public int length { get; set; }
        public double[] segBounds { get; set; } = new double[2] {0.0, 0.0};
        public double step { get; set; }
        public VMf funcType { get; set; }

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
