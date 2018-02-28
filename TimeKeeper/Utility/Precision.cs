using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Helper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Precision : Attribute
    {
        public byte precision { get; set; }
        public byte scale { get; set; }

        public Precision(byte precision, byte scale)
        {
            this.precision = precision;
            this.scale = scale;
        }
    }
}
