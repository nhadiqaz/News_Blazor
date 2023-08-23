using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Log
    {
        public Log(string message)
        {
            Message = message;
            TimeStamp = DateTime.Now;
        }

        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
