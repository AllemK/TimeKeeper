using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public class Member : BaseClass<int>
    {
        public int Hours { get; set; }
    }
}
