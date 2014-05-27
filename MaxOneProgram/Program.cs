using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace MaxOneProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer();
            timer.Enable = true;
            timer.Interval = 1000;
            timer.Ticked += new EventHandler(Timerd);
        }

         void Timerd(object sender,EventArgs e)
        {
        }
    }
}
