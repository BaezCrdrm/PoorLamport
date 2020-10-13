using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoorLamportTest.Models
{
    public class Message
    {
        public Process Parent { get; set; }
        public Process Target { get; set; }
        public int Time { get; set; }
        public String Text { get; set; }

        public Message()
        {
            Time = 0;
        }
    }
}
