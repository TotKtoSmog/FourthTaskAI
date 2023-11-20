using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthTaskAI
{
    internal class Cell
    {
        internal int value { get; set; }
        internal char texture { get; set; }
        internal Cell() { }
        internal Cell(int value, char texture)
        {
            this.value = value;
            this.texture = texture;
        }
    }
}
