using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoorLamportTest.Models
{
    public class Process
    {
        public int ID { get; }
        public int Clock { get; set; }

        public Process(int id)
        {
            this.ID = id;
            Clock = 0;
        }
    }
}
